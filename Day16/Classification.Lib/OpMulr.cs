namespace Classification.Lib
{
    public class OpMulr : Operator
    {
        public OpMulr(int[] initState)
            : base(initState)
        {
            opId = "opMulr";
            OpCode = 4;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] * _copyOfRegisters[valB];

            after = _copyOfRegisters;
        }
    }
}