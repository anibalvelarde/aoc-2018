namespace Classification.Lib
{
    public abstract class Operator
    {
        protected int[] _copyOfRegisters = { 0, 0, 0, 0 };
        protected int valA;
        protected int valB;
        protected int valC;


        public Operator(int[] initialState)
        {
            _copyOfRegisters = initialState;
            OpCode = -1;
        }
        public virtual string opId { get; protected set; }
        public virtual int OpCode { get; protected set; }

        public abstract void Execute(int[] instruction, out int[] after);
        public virtual void Execute(int[] instruction)
        {
            valA = instruction[1];
            valB = instruction[2];
            valC = instruction[3];
        }
        public virtual void TryExecute(int[] registers, int[] instruction, out int[] result)
        {
            _copyOfRegisters = registers;
            Execute(instruction, out result);
        }

        public override string ToString()
        {
            return $"R1[{_copyOfRegisters[0]}] R2[{_copyOfRegisters[1]}] R3[{_copyOfRegisters[2]}] R4[{_copyOfRegisters[3]}]";
        }
    }
}
