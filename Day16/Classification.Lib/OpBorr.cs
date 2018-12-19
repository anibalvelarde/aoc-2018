namespace Classification.Lib
{
    public class OpBorr : Operator
    {
        public OpBorr(int[] initState)
            : base(initState)
        {
            opId = "opBorr";
            OpCode = 14;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] | _copyOfRegisters[valB];

            after = _copyOfRegisters;
        }
    }
}