using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TFTP
{
    class Client : Udp
    {
        #region CONSTRUCTOR
        public Client(string server)
            : base(new IPEndPoint(IPAddress.Parse(server), 69))
        {
            Console.WriteLine("myTFTP Client: ");
            
        }
        #endregion
        #region COMMUNICATION
        public override void communication(Operation op, string target)
        {
            switch (op)
            {
                case Operation.RRQ:
                    Read(target);
                    break;
                case Operation.WRQ:
                    Write(target);
                    break;
                default:
                    break;
            }
        }
        /*
         * NOT IMPLEMENTED SERVER COMMUNICATION
         * */
        public override void communication()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region FUNCTIONS
        public override void Read(string target)
        {
            try
            {
                Packet pck = new Packet(Operation.RRQ, target, "OCTET");
                Console.WriteLine(pck.ToString());
                sock.SendTo(pck.Bytes, localEndPoint);
                EndPoint test = localEndPoint;
                sock.ReceiveTimeout = 1;
                while (sock.ReceiveFrom(Bytes, ref test) != 0 && Bytes[1] == 0x3)
                {
                    pck.Bytes = Bytes;
                    Console.WriteLine(pck.ToString());
                    byte[] bloc = new byte[2]{Bytes[2],Bytes[3]};
                    pck = new Packet(bloc);
                    Console.WriteLine(pck.ToString());
                    sock.SendTo(pck.Bytes, test);
                }
                if (Bytes[1] == 0x5)
                {
                    pck.Bytes = Bytes;
                    Console.WriteLine(pck.ToString());
                }
                    
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Connection failed: " + ex.ErrorCode);
            }
            finally
            {
                sock.Close();
                Console.WriteLine("Finish");
            }
            
        }
        public override void Write(string target)
        {
            try
            {
                Packet pck = new Packet(Operation.WRQ, target+"(copy)", "OCTET");
                //Console.WriteLine(pck.ToString());
                sock.SendTo(pck.Bytes, localEndPoint);
                EndPoint test = localEndPoint;
                sock.ReceiveTimeout = 1;
                if (File.Exists(target))
                {
                    FileStream f = File.OpenRead(target);
                    try
                    {
                        byte[] send = new byte[512];
                        for (int j = 0; j < f.Length / 512 + 1; j++)
                        {
                            f.Read(send, 0, 512);
                            byte[] b = BitConverter.GetBytes(j);
                            pck = new Packet(b, send);
                            Console.WriteLine(pck.ToString());
                            sock.SendTo(pck.Bytes, test);
                            sock.ReceiveFrom(Bytes, ref test);
                            pck.Bytes = Bytes;
                            Console.WriteLine(pck.ToString());
                            if (BitConverter.ToInt16(Bytes, 2) != j)
                                return;
                        }
                    }
                    finally
                    {
                        f.Close();
                    }

                }
                else
                {
                    byte[] b = new byte[2] { 0x0, 0x1 };
                    pck = new Packet(b, "File_don't_exist");
                    sock.SendTo(pck.Bytes, test);
                    Console.WriteLine(pck.ToString());

                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine("Connection failed: " + ex.ErrorCode);
            }
            finally
            {
                sock.Close();
                Console.WriteLine("Finish");
            }
        }
        #endregion
    }
}
