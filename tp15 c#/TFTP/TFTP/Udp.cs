using System;
using System.Net;
using System.Net.Sockets;

namespace TFTP
{
    abstract class Udp
    {
        #region VARIABLE
        public byte[] Bytes;
        protected Socket sock;
        protected IPEndPoint localEndPoint;
        protected EndPoint test;
        #endregion
        #region CONSTRUCTOR
        public Udp(IPEndPoint localEndPoint)
        {
            this.localEndPoint = localEndPoint;
            Bytes = new Byte[1024];
            sock = new Socket(localEndPoint.Address.AddressFamily,
                                         SocketType.Dgram,
                                         ProtocolType.Udp
                                         );
        }
        #endregion
        #region FUNCTION
        public abstract void Read(string target);
        public abstract void Write(string target);
        public abstract void communication();
        public abstract void communication(Operation op, string target);
        #endregion
    }
}
