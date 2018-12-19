namespace Classification.Lib
{
    public class OpBori : Operator
    {
        public OpBori(int[] initState)
            : base(initState)
        {
            opId = "opBori";
            OpCode = 8;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] | valB;

            after = _copyOfRegisters;
        }
    }
}