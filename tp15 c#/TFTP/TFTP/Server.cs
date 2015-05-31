using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TFTP
{
    class Server : Udp
    {
        public Server()
            : base(new IPEndPoint(IPAddress.Any, 69))
        {
            Console.WriteLine("myTFTP Server: Ready to receive");
            
        }
        private void startListening(EndPoint test)
        {
            sock.ReceiveFrom(Bytes, ref test);
            Console.WriteLine("new Connection");
        }
        public override void Read(string target)
        {
            try
            {
                EndPoint test = localEndPoint;
                sock.Bind(localEndPoint);
                sock.ReceiveFrom(Bytes, ref test);
                int i = 2;
                while (Bytes[i] != 0)
                    i++;
                string Filename = System.Text.Encoding.ASCII.GetString(Bytes, 2, i - 2);
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
            throw new NotImplementedException();
        }
    }
}
