using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TFTP
{
	class MainClass
	{
        const string HELP = "missing argument -h for help";
		public static void Main (string[] args)
		{
            Console.Clear();
            if (args.Length >= 1)
            {
                Udp udp;
                switch (args[0])
                {
                    case "-s":
                        udp = new Server();
                        udp.communication();
                        break;
                    case "-c":
                        if (args.Length < 3)
                        {
                            Console.WriteLine(HELP);
                            return;
                        }
                        udp = new Client("127.0.0.1");
                        switch (args[1])
	                    {
                            case "-R":
                                udp.communication(Operation.RRQ, args[2]);
                                break;
                            case "-W":
                                udp.communication(Operation.WRQ, args[2]);
                                break;
                            default:
                                Console.WriteLine(HELP);
                                return;
	                    }
                        break;
                    case "-h":
                        Console.WriteLine("help:");
                        Console.WriteLine("Usage: myTFTP [-E [-e <key >]] <operation > <mode > <target >");
                        Console.WriteLine("mode: Client (-c) or Server (-s)");
                        Console.WriteLine("operation: Read (-R) or Write (-W)");
                        Console.WriteLine("file: In Server mode this is the working directory ,In Client mode IP:Path");
                        break;
                    default:
                        Console.WriteLine(HELP);
                            return;
                }
            }
            else
            {
                Console.WriteLine(HELP);
            }
		}
	}
}
