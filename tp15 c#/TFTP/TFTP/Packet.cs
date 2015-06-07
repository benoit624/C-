using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFTP
{
    public enum Operation
    {
        RRQ=1,
        WRQ,
        DATA,
        ACK,
        ERROR
    }
    class Packet
    {
        #region VARIABLE
        public byte[] Bytes;
        #endregion
        #region CONSTRUCTOR
        public Packet(Operation op, string Filename, string Mode)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(Filename);
            byte[] buffer2 = System.Text.Encoding.ASCII.GetBytes(Mode);
            Bytes = new byte[4 + buffer.Length + buffer2.Length];
            Bytes[1] = (byte)op;
            int i = 2;
            for (int j = 0; j < buffer.Length; j++, i++)
                Bytes[i] = buffer[j];
            i++;
            for (int j = 0; j < buffer2.Length; j++, i++)
                Bytes[i] = buffer2[j];
        }//RRQ\WRQ
        public Packet(byte[] Bloc, byte[] DATA)
        {
            Bytes = new byte[4 + DATA.Length];
            Bytes[1] = 0x3;
            int j = 2;
            for (int i = 0; i < 2; i++,j++)
                Bytes[j] = Bloc[i];
            for (int i = 0; i < DATA.Length; i++, j++)
                Bytes[j] = DATA[i];
        }//DATA
        public Packet(byte[] Bloc)
        {
            Bytes = new byte[4];
            Bytes[1] = 0x4;
            Bytes[2] = Bloc[0];
            Bytes[3] = Bloc[1];
        }//ACK
        public Packet(byte[] Error, string Msg)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(Msg);
            Bytes = new byte[5 + buffer.Length];
            Bytes[1] = 0x5;
            int j = 2;
            for (int i = 0; i < 2; i++, j++)
                Bytes[j] = Error[i];
            for (int i = 0; i < buffer.Length; i++, j++)
                Bytes[j] = buffer[i];
        }//ERROR
        #endregion
        #region FUNCTION
        public override string ToString()
        {
            string str = new String('-', 30);
            str += "\nType:    ";
            switch (Bytes[1])
            {
                case 0x1:
                    str += "RRQ";
                    str += "\nFile:    ";
                    int i = 2;
                    while (Bytes[i] != 0)
                        i++;
                    str += System.Text.Encoding.ASCII.GetString(Bytes, 2, i - 2);
                    str += "\nMode:    ";
                    int j = i;
                    while (Bytes[j] != 0)
                        j++;
                    str += System.Text.Encoding.ASCII.GetString(Bytes, i, j - 1);
                    break;
                case 0x2:
                    str += "WRQ";
                    str += "\nFile:    ";
                    i = 2;
                    while (Bytes[i] != 0)
                        i++;
                    str += System.Text.Encoding.ASCII.GetString(Bytes, 2, i - 2);
                    str += "\nMode:    ";
                    j = i;
                    while (Bytes[j] != 0)
                        j++;
                    str += System.Text.Encoding.ASCII.GetString(Bytes, i, j - 1);
                    break;
                case 0x3:
                    str += "DATA";
                    str += "\nBlock:   ";
                    str += BitConverter.ToInt16(Bytes, 2);
                    str += "\nData:    \n";
                    str += System.Text.Encoding.ASCII.GetString(Bytes, 4, Bytes.Length - 4);

                    break;
                case 0x4:
                    str += "ACK";
                    str += "\nBlock:   ";
                    str += BitConverter.ToInt16(Bytes, 2);
                    break;
                case 0x5:
                    str += "ERROR";
                    str += "\nERROR:   ";
                    str += BitConverter.ToInt16(Bytes, 2);
                    str += "\nINFO:    ";
                    str += System.Text.Encoding.ASCII.GetString(Bytes, 4, Bytes.Length - 5);
                    break;
                default:
                    break;
            }
            return str;
        }
        #endregion
    }
}
