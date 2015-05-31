using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TFTP
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Console.Clear();
            if (args.Length >= 3)
            {
                Udp udp;
                switch (args[0])
                {
                    case "-s":
                        udp = new Server();
                        break;
                    case "-c":
                        udp = new Client("127.0.0.1");
                        break;
                    default:
                        Console.WriteLine("argument don't exist");
                        return;
                        throw (new Exception("argument don't exist"));
                }
                switch (args[1])
                {
                    case "-R":
                        udp.Read(args[2]);
                        break;
                    case "-W":
                        udp.Write(args[2]);
                        break;
                    default:
                        Console.WriteLine("argument don't exist");
                        return;
                        throw (new Exception("argument don't exist"));
                }
            }
            else if (args.Length == 1 && args[0]=="-h")
            {
                Console.WriteLine("help:");
                Console.WriteLine("Usage: myTFTP [-E [-e <key >]] <operation > <mode > <target >");
                Console.WriteLine("mode: Client (-c) or Server (-s)");
                Console.WriteLine("operation: Read (-R) or Write (-W)");
                Console.WriteLine("file: In Server mode this is the working directory ,In Client mode IP:Path");
            }
            else
            {
                Console.WriteLine("missing argument -h for help");
            }
		}
	}
}
