using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TFTP
{
    class Client : Udp
    {
        public Client(string server)
            : base(new IPEndPoint(IPAddress.Parse(server), 69))
        {
            Console.WriteLine("myTFTP Client: ");
            
        }
        public void send(string Filename)
        {
            if (File.Exists(Filename))
            {
                try
                {
                    EndPoint test = (EndPoint)localEndPoint;
                    sock.SendTo(Bytes, test);
                    Console.WriteLine("Connection success");
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
            else
                Console.WriteLine("File don't exist");
            
        }
        public byte[] loadfile(string filename)
        {
            if (File.Exists(filename))
                return File.ReadAllBytes(filename);
            else
                return null;
        }
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
            throw new NotImplementedException();
        }
    }
}
