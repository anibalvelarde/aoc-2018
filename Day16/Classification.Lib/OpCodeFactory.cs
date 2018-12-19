using System;

namespace Classification.Lib
{
    public class OpCodeFactory
    {
        public static OpAddr MakeAddr(int[] copyOfRegs)
        {
            return new OpAddr(copyOfRegs);
        }
        public static OpAddi MakeAddi(int[] copyOfRegs)
        {
            return new OpAddi(copyOfRegs);
        }
        public static OpMulr MakeMulr(int[] copyOfRegs)
        {
            return new OpMulr(copyOfRegs);
        }
        public static OpMuli MakeMuli(int[] copyOfRegs)
        {
            return new OpMuli(copyOfRegs);
        }
        public static OpBorr MakeBorr(int[] copyOfRegs)
        {
            return new OpBorr(copyOfRegs);
        }
        public static OpBori MakeBori(int[] copyOfRegs)
        {
            return new OpBori(copyOfRegs);
        }
        public static OpBanr MakeBanr(int[] copyOfRegs)
        {
            return new OpBanr(copyOfRegs);
        }
        public static OpBani MakeBani(int[] copyOfRegs)
        {
            return new OpBani(copyOfRegs);
        }
        public static OpSetr MakeSetr(int[] copyOfRegs)
        {
            return new OpSetr(copyOfRegs);
        }
        public static OpSeti MakeSeti(int[] copyOfRegs)
        {
            return new OpSeti(copyOfRegs);
        }
        public static OpGtir MakeGtir(int[] copyOfRegs)
        {
            return new OpGtir(copyOfRegs);
        }
        public static OpGtri MakeGtri(int[] copyOfRegs)
        {
            return new OpGtri(copyOfRegs);
        }
        public static OpGtrr MakeGtrr(int[] copyOfRegs)
        {
            return new OpGtrr(copyOfRegs);
        }
        public static OpEqir MakeEqir(int[] copyOfRegs)
        {
            return new OpEqir(copyOfRegs);
        }
        public static OpEqri MakeEqri(int[] copyOfRegs)
        {
            return new OpEqri(copyOfRegs);
        }
        public static OpEqrr MakeEqrr(int[] copyOfRegs)
        {
            return new OpEqrr(copyOfRegs);
        }
    }
}
