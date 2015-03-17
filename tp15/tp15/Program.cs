using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
namespace tp15
{
    class Program
    {
        public static void Main(String[] args)
        {
            TcpClient tcpClient = new TcpClient ();
            IPEndPoint serverAddress = new IPEndPoint (IPAddress . Parse ("127.0.0.1"), 80);
            tcpClient.Connect ( serverAddress );
            byte [] msg = some_function_you_have_to_code ("My string message ");
            NetworkStream ns = tcpClient.GetStream ();
            ns.Write (msg , 0, msg.Length );
            ns.Flush ();
            Stopwatch clock = new Stopwatch ();
            clock.Start();
            byte [] ans = new byte [4096];
            int ans_size = 0;
            while (clock.ElapsedMilliseconds < 5000)
            {
                if (ns.DataAvailable )
                {
                    ans_size = ns.Read (ans , 0, 4096) ;
                    break ;
                }
            }
            Console . WriteLine (" Message received ( length = {0}) : {1}",ans_size , some_function_you_have_to_code (ans));
        }
        public static string some_function_you_have_to_code(byte[] msg)
        {
            string result = "";
            for (int i = 0; i < msg.Length; i++)
            {
                result += (char)msg[i];
            }
            return result;
        }
        public static byte[] some_function_you_have_to_code(string msg)
        {
            byte [] result=new byte[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                result[i] = (byte)msg[i];
            }
            return result;
        }
    }
}
