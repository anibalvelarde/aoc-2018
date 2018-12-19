namespace Classification.Lib
{
    public class OpSeti: Operator
    {
        public OpSeti(int[] initState)
            : base(initState)
        {
            opId = "seti";
            OpCode = 5;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = valA;

            after = _copyOfRegisters;
        }
    }
}
