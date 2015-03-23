using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
namespace CTF
{
    class Client
    {
        private TcpClient tcpClient;
        private IPEndPoint serverAddress;
        private NetworkStream ns;
        private Stopwatch clock;
        protected const string login = "forget_c";
        bool connection;
        public Client(string add,int port)
        {
            connection = false;
            try
            {
                tcpClient = new TcpClient();
                serverAddress = new IPEndPoint(IPAddress.Parse(add), port);
                clock = new Stopwatch();
                
            }
            catch
            {
                Console.WriteLine("Adresse ip erronee");
            }
            
        }
        public bool connect()
        {
            if (!connection)
            {
                try
                {
                    Console.WriteLine("serveur connecté");
                    tcpClient.Connect(serverAddress);
                    return true;
                }
                catch
                {
                    Console.WriteLine("Impossible de se connecter au serveur");
                    return false;
                }
            }
            return true;
        }
        public void send(string msg)
        {
            if (connection)
            {
                Console.WriteLine("=>  \"" + msg + "\"");
                ns = tcpClient.GetStream();
                ns.Write(trad(msg), 0, msg.Length);
                ns.Flush();
            }
            
        }
        public string receive()
        {
            clock.Start();
            byte[] ans = new byte[4096];
            int ans_size = 0;
            while (clock.ElapsedMilliseconds < 5000)
            {
                if (ns.DataAvailable)
                {
                    ans_size = ns.Read(ans, 0, 4096);
                    break;
                }
            }
            string msg = trad(ans);
            Console.WriteLine("<=  \"" + msg + "\"");
            return msg;
        }
        private string trad(byte [] msg)
        {
            string result = "";
            for (int i = 0; i < msg.Length; i++)
            {
                if(msg[i]!=0)
                    result += (char)msg[i];
            }
            return result;
        }
        private byte[] trad(string msg)
        {
            byte[] result = new byte[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                result[i] = (byte)msg[i];
            }
            return result;
        }
        public bool process(ExX ex,string str)
        {
            connection = connect();
            string answer = "reconnection";
            try
            {
                send(ex.format(str));
                answer = receive();
            }
            catch (Exception)
            {
                connection = false;
            }
            switch (answer)
            {
                case "OK:Sucess!":
                    return true;
                case "OK:Success!":
                    return true;
                case"OK:GGWP":
                    return true;
                case "OK:Well done!":
                    return true;
                case "OK:EZ":
                    return true;
                case "KO:Fail!":
                    return false;
                case"":
                    return false;
                default:
                    return process(ex, ex.solve(answer));
            }
        }
        public bool process(List<ExX> ex)
        {
            foreach (ExX exo in ex)
            {
                Console.WriteLine();
                process(exo, "");
            }
            return true;
        }
    }
}
