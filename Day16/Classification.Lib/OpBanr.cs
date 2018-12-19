namespace Classification.Lib
{
    public class OpBanr : Operator
    {
        public OpBanr(int[] initState)
            : base(initState)
        {
            opId = "opBanr";
            OpCode = 15;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] & _copyOfRegisters[valB];

            after = _copyOfRegisters;
        }
    }
}