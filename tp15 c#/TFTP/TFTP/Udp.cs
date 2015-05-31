using System;
using System.Net;
using System.Net.Sockets;

namespace TFTP
{
    abstract class Udp
    {
        public byte[] Bytes;
        protected Socket sock;
        protected IPEndPoint localEndPoint;
        protected delegate void sending(string Filename);
        public Udp(IPEndPoint localEndPoint)
        {
            this.localEndPoint = localEndPoint;
            Bytes = new Byte[1024];
            sock = new Socket(localEndPoint.Address.AddressFamily,
                                         SocketType.Dgram,
                                         ProtocolType.Udp
                                         );
        }
        public abstract void Read(string target);
        public abstract void Write(string target);
        private void send(sending send)
        {

        }


    }
}
