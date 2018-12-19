namespace Classification.Lib
{
    public class OpMuli : Operator
    {
        public OpMuli(int[] initState)
            : base(initState)
        {
            opId = "opMuli";
            OpCode = 6;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] * valB;

            after = _copyOfRegisters;
        }
    }
}