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
    class Program
    {
        public static void Main(String[] args)
        {
            //int secret=15;
            //Console.WriteLine(Server.solve_1("-1"));
            //Server.solve_2("0", ref secret, 10);
            //Server.solve_2("0", ref secret, 10);
            //Server.solve_2(Console.ReadLine(), ref secret, 10);
            Client client = new Client("10.3.6.36", 4242);
            client = new Client("91.121.83.195", 4242);
            client = new Client("80.236.44.146", 6969);
            Ex0 ex0 = new Ex0();
            //client.process(ex0);
            List<ExX> exos = new List<ExX>();
            exos.Add(ex0);
            exos.Add(new Ex1());
            exos.Add(new Ex2());
            exos.Add(new Ex3());
            exos.Add(new Ex4());
            exos.Add(new Ex5());
            exos.Add(new Ex6());
            exos.Add(new Ex7());
            exos.Add(new Ex8());
            exos.Add(new Ex9());
            exos.Add(new ExA());
            exos.Add(new ExB());
            exos.Add(new ExC());
            exos.Add(new ExD());
            client.process(exos);
            Console.ReadLine();

        }
    }
}
