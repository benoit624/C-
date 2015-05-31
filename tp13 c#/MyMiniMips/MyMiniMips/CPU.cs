#define DEBUGGER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyMiniMips
{
    public enum Register
    {
        zero,
        at,
        v0,
        v1,
        a0,
        a1,
        a2,
        a3,
        t0,
        t1,
        t2,
        t3,
        t4,
        t5,
        t6,
        t7,
        s0,
        s1,
        s2,
        s3,
        s4,
        s5,
        s6,
        s7,
        t8,
        t9,
        k0,
        k1,
        gp,
        sp,
        fp,
        ra

    }
    public enum registre : int { PrintInt = 1, PrintString = 4, ReadInt = 5, Exit = 10, PrintChar = 11 };
	class CPU
	{
		public byte[] RAM;
        public int[] REGISTER;
        public int HI;
        public int LO;
		public int Program_Counter;
		public CPU(string fileName)
		{
			RAM = new byte[64 * 1024];
			REGISTER = new int[32];
			Program_Counter = 0;
			LoadFile(fileName);
		}
		public void LoadFile(string fileName)
		{
			using (FileStream fs = File.OpenRead(fileName))
			{
				fs.Read(RAM,0,RAM.Length); 
			}
		}
        public int register(Register r)
        {
            return REGISTER[(int)r];
        }
	}
	class Instruction 
	{
		public short Op { get; private set; }
        public short Rs { get; private set; }
        public short Rt { get; private set; }
        public short Rd { get; private set; }
        public short shamt { get; private set; }
        public short Funct { get; private set; }
		public short Imm { get; private set; }
		public int Addr { get; private set; }
		public Instruction(int ins)
		{
			Op = (short) (ins >> 26);
            Rs = (short) (ins >> 21 & 0x1F);
            Rt = (short) (ins >> 16 & 0x1F);
            Rd = (short) (ins >> 11 & 0x1F);
            shamt = (short) (ins >> 6 & 0x1F);
            Funct = (short) (ins & 0x3F);
			Imm = (short) (ins & 0xFFFF);
            Addr = ins & 0x3FFFFFF;
		}
	}
	class ALU
	{
        public static string printreg(int i)
        {
            switch (i)
            {
                case 0:
                    return "zero";
                case 1:
                    return "at";
                case 2:
                    return "v0";
                case 3:
                    return "v1";
                case 4:
                    return "a0";
                case 5:
                    return "a1";
                case 6:
                    return "a2";
                case 7:
                    return "a3";
                case 8:
                    return "t0";
                case 9:
                    return "t1";
                case 10:
                    return "t2";
                case 11:
                    return "t3";
                case 12:
                    return "t4";
                case 13:
                    return "t5";
                case 14:
                    return "t6";
                case 15:
                    return "t7";
                case 16:
                    return "s0";
                case 17:
                    return "s1";
                case 18:
                    return "s2";
                case 19:
                    return "s3";
                case 20:
                    return "s4";
                case 21:
                    return "s5";
                case 22:
                    return "s6";
                case 23:
                    return "s7";
                case 24:
                    return "t8";
                case 25:
                    return "t9";
                case 26:
                    return "k0";
                case 27:
                    return "k1";
                case 28:
                    return "gp";
                case 29:
                    return "sp";
                case 30:
                    return "fp";
                case 31:
                    return "ra";
                default:
                    return "error";
            }
        }
		public CPU cpu;
		public ALU(CPU cpu)
		{
			this.cpu = cpu;
            while ( cpu.Program_Counter < cpu.RAM.Length)
            {
                int r = BitConverter.ToInt32(cpu.RAM, cpu.Program_Counter);
                Exec(new Instruction(r));
                cpu.Program_Counter += 4;
            }
                
		}
        /*
         * int a = cpu.register(Register.a0);
         * for (int j = 31; j >= 0; j--)
         *               Console.Write(((a >> j) & 1));
         * */
        public bool ExecSyscall(Instruction i)
        {
            #region DEBUGGER
#if DEBUGGER
            Console.WriteLine("syscall");
#endif
            #endregion
            switch (cpu.register(Register.v0)) //$v0
			{
				case 1 :
                    Console.Write(cpu.register(Register.a0));
					break;
				case 4 :
                    int deb = cpu.register(Register.a0);
                    while (deb < 64 * 1024 && cpu.RAM[deb] != cpu.register(Register.zero)) 
                    {
                        Console.Write((char)cpu.RAM[deb]);
                        deb++;
                    }
					break;
				case 5:
                    int read;
                    if (Int32.TryParse(Console.ReadLine(), out read))
                        cpu.REGISTER[(int)Register.v0] = read;
					break;
				case 10:
                    Console.ReadLine();
                    Environment.Exit(0);
					break;
				case 11:
                    Console.Write((char)cpu.RAM[cpu.register(Register.a0)]);
					break;
				default:
					return false;
			}
			return true;
		}
		public bool ExecIJ(Instruction i)
		{
            int counter;
            switch (i.Op)
            {
                case 2:
                    #region jump
                    counter = (int)((cpu.Program_Counter & 0xf0000000) | (i.Addr << 2));
                    cpu.Program_Counter = counter - 0x400004;
                    #region DEBUGGER
#if DEBUGGER
                    Console.Write("j 0x");
                    MainClass.hexprint(counter, 8);
#endif
                    #endregion
                    #endregion
                    break;
                case 4:
                    #region beq
                    #region DEBUGGER
#if DEBUGGER
                    Console.Write("beq ${0}, ${1}, 0x", printreg(i.Rs), printreg(i.Rt));
                    MainClass.hexprint(cpu.Program_Counter + 0x400000 + (i.Imm << 2), 8);
#endif
                    #endregion
                    if (cpu.REGISTER[i.Rs] == cpu.REGISTER[i.Rt])
                        cpu.Program_Counter += (i.Imm << 2);
                    #endregion
                    break;
                case 5:
                    #region bne
                    if (cpu.REGISTER[i.Rs] != cpu.REGISTER[i.Rt])
                        cpu.Program_Counter += (i.Imm << 2);
                    #region DEBUGGER
#if DEBUGGER
                    Console.Write("bne ${0}, ${1}, 0x", printreg(i.Rs), printreg(i.Rt));
                    MainClass.hexprint(cpu.Program_Counter + 0x400000 + (i.Imm << 2), 8);
#endif
                    #endregion
                    #endregion
                    break;
                case 8:
                    #region addi
                    cpu.REGISTER[i.Rt] = cpu.REGISTER[i.Rs] + i.Imm;
                    #region DEBUGGER
#if DEBUGGER
                    Console.Write("addi ${0}, ${1}, {2}", printreg(i.Rt), printreg(i.Rs), i.Imm);
#endif
                    #endregion
                    #endregion
                    break;
                case 9 :
                    #region addiu
                    cpu.REGISTER[i.Rt] = cpu.REGISTER[i.Rs] + i.Imm;
                    #region DEBUGGER
#if DEBUGGER
                    Console.Write("addiu ${0}, ${1}, {2}", printreg(i.Rt), printreg(i.Rs), i.Imm);
#endif
                    #endregion
                    #endregion
                    break;
                case 15:
                    #region lui
                    cpu.REGISTER[i.Rt] = i.Imm << 16;
                    #region DEBUGGER
#if DEBUGGER
                    Console.Write("lui ${0}, 0x", printreg(i.Rt));
                    MainClass.hexprint(i.Imm, 8);
#endif
                    #endregion
                    #endregion
                    break;
                case 13:
                    #region ori
                    cpu.REGISTER[i.Rt] = cpu.REGISTER[i.Rs] | i.Imm;
                    #region DEBUGGER
#if DEBUGGER
                    Console.Write("ori ${0}, ${1}, 0x{2}", printreg(i.Rt), printreg(i.Rs), i.Imm);
#endif
                    #endregion
                    #endregion
                    break;
                default:
                    return false;
            }
			return true;
		}
        public void ExecR(Instruction i)
        {
            string act;
            switch (i.Funct)
            {
                case 8 :
                    act = "jr";
                    cpu.Program_Counter = cpu.REGISTER[i.Rs] - 0x4;
                    break;
                case 16:
                    act = "mfhi";
                    cpu.REGISTER[i.Rd] = cpu.HI;
                    break;
                case 18:
                    act = "mflo";
                    cpu.REGISTER[i.Rd] = cpu.LO;
                    break;
                case 24:
                    act = "mult";
                    cpu.LO = cpu.REGISTER[i.Rs] * cpu.REGISTER[i.Rt];
                    break;
                case 25:
                    act = "multu";
                    cpu.LO = cpu.REGISTER[i.Rs] * cpu.REGISTER[i.Rt];
                    break;
                case 26:
                    act = "div";
                    cpu.LO = cpu.REGISTER[i.Rs] / cpu.REGISTER[i.Rt];
                    cpu.HI = cpu.REGISTER[i.Rs] % cpu.REGISTER[i.Rt];
                    break;
                case 27:
                    act = "divu";
                    cpu.LO = cpu.REGISTER[i.Rs] / cpu.REGISTER[i.Rt];
                    cpu.HI = cpu.REGISTER[i.Rs] % cpu.REGISTER[i.Rt];
                    break;
                case 32 :
                    cpu.REGISTER[i.Rd] =  (cpu.REGISTER[i.Rs] + cpu.REGISTER[i.Rt]);
                    act = "add";
                    break;
                case 33:
                    cpu.REGISTER[i.Rd] = (cpu.REGISTER[i.Rs] + cpu.REGISTER[i.Rt]);
                    act = "addu";
                    break;
                case 34:
                    cpu.REGISTER[i.Rd] = (cpu.REGISTER[i.Rs] - cpu.REGISTER[i.Rt]);
                    act = "sub";
                    break;
                case 36 : 
                    act = "and";
                    cpu.REGISTER[i.Rd] = cpu.REGISTER[i.Rs] & cpu.REGISTER[i.Rd];
                    break;
                case 37:
                    act = "or";
                    cpu.REGISTER[i.Rd] = cpu.REGISTER[i.Rs] | cpu.REGISTER[i.Rd];
                    break;
                case 39:
                    act = "nor";
                    cpu.REGISTER[i.Rd] = (cpu.REGISTER[i.Rs] | cpu.REGISTER[i.Rd]) ^ 0xFFFFFFF;
                    break;
                default:
                    act = "error funct";
                    break;
            }
#if DEBUGGER
            Console.Write("{0} ${1}, ${2}, ${3}",act, printreg(i.Rd), printreg(i.Rs), printreg(i.Rt));
#endif
        }
		public void Exec(Instruction i )
		{
#if DEBUGGER
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[my_minimips] Executing pc ");
            Console.ResetColor();
            Console.Write("= ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("0x");
            MainClass.hexprint(cpu.Program_Counter + 0x400000, 8);
            Console.ResetColor();
            Console.Write(": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("0x");
            MainClass.hexprint(cpu.RAM[cpu.Program_Counter], 8);
            Console.ResetColor();
            Console.Write(": ");
#endif
            if (i.Op == 0)
            {
                if (i.Funct == 12)
                    ExecSyscall(i);
                else
                    ExecR(i);
            }
            else
                ExecIJ(i);

		}
	}
	class Exec
	{

	}
}
