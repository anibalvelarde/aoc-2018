namespace Classification.Lib
{
    public class OpBani : Operator
    {
        public OpBani(int[] initState)
            : base(initState)
        {
            opId = "opBani";
            OpCode = 9;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] & valB;

            after = _copyOfRegisters;
        }
    }
}