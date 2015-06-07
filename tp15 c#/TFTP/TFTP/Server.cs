using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TFTP
{
    class Server : Udp
    {
        #region CONSTRUCTOR
        public Server()
            : base(new IPEndPoint(IPAddress.Any, 69))
        {
            Console.WriteLine("myTFTP Server: Ready to receive");
            
        }
        #endregion
        #region COMMUNICATION
        public override void communication()
        {
            try
            {
                test = localEndPoint;
                sock.Bind(localEndPoint);
                sock.ReceiveFrom(Bytes, ref test);
                int i = 2;
                while (Bytes[i] != 0)
                    i++;
                string Filename = System.Text.Encoding.ASCII.GetString(Bytes, 2, i - 2);
                switch (Bytes[1])
                {
                    case 0x1:
                        Read(Filename);
                        break;
                    case 0x2:
                        Write(Filename);
                        break;
                    default:
                        break;
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Connection end: " + ex.ErrorCode);
            }
            finally
            {
                sock.Close();
            }
        }
        /*
         * NOT IMPLEMENTED CLIENT COMMUNICATION
         * */
        public override void communication(Operation op, string target)
        {
            throw new NotImplementedException();
        }        
        #endregion
        #region FUNCTION
        public override void Read(string Filename)
        {
                if (File.Exists(Filename))
                {
                    FileStream f = File.OpenRead(Filename);
                    try 
                    {
                        byte[] send = new byte[512];
                        for (int j = 0; j < f.Length/512+1; j++)
                        {
                            f.Read(send,0,512);
                            byte[] b = BitConverter.GetBytes(j);
                            Packet pck = new Packet(b, send);
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
                    byte[] b = new byte[2]{0x0,0x1};
                    Packet pck = new Packet(b, "File_don't_exist");
                    sock.SendTo(pck.Bytes, test);
                    Console.WriteLine(pck.ToString());
                    
                }
            
        }
        public override void Write(string Filename)
        {
            if (!File.Exists(Filename))
            {
                FileStream f = File.OpenWrite(Filename);
                try
                {
                    
                    byte[] send = new byte[512];
                    sock.ReceiveTimeout = 3;
                    while (sock.ReceiveFrom(Bytes, ref test) != 0 && Bytes[1] == 0x3)
                    {
                        f.Write(Bytes, 4, 512);
                        byte[] bloc = new byte[2] { Bytes[2], Bytes[3] };
                        Packet pck = new Packet(bloc);
                        Console.WriteLine(pck.ToString());
                        sock.SendTo(pck.Bytes, test);
                    }
                    f.Flush();
                     
                }
                finally
                {
                    f.Close();
                }

            }
            else
            {
                byte[] b = new byte[2] { 0x0, 0x1 };
                Packet pck = new Packet(b, "File exist");
                sock.SendTo(pck.Bytes, test);
                Console.WriteLine(pck.ToString());

            }
        }
        #endregion
    }
}
