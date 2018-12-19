namespace Classification.Lib
{
    public class OpAddr : Operator
    {
        public OpAddr(int[] initState)
            : base(initState)
        {
            opId = "addr";
            OpCode = 12;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] + _copyOfRegisters[valB];

            after = _copyOfRegisters;
        }
    }
}
