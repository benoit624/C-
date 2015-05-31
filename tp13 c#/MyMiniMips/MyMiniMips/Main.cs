using System;

namespace MyMiniMips
{
	class MainClass
	{
        public static void hexprint(int elt,int nb)
        {
            for (int i = (nb-1)*4; i >= 0; i-=4)
            {
                int a = ((elt >> i) & 0xF);
                char ret=' ';
                switch (a)
                {
                    case 10:
                        ret = 'A';
                        break;
                    case 11:
                        ret = 'B';
                        break;
                    case 12:
                        ret = 'C';
                        break;
                    case 13:
                        ret = 'D';
                        break;
                    case 14:
                        ret = 'E';
                        break;
                    case 15:
                        ret = 'F';
                        break;
                    default:
                        Console.Write(a);
                        break;
                }
                if (a > 9) 
                    Console.Write(ret);   
            }
        }
		public static void Main (string[] args)
		{
            CPU cpu = new CPU("fibo");
            ALU alu = new ALU(cpu);
            alu.Exec(new Instruction(0x00004020));
            Console.ReadLine();
		}
	}
}
