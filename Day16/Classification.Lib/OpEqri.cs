namespace Classification.Lib
{
    public class OpEqri : Operator
    {
        public OpEqri(int[] initState)
            : base(initState)
        {
            opId = "opEqri";
            OpCode = 7;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            if (_copyOfRegisters[valA] == valB)
            {
                _copyOfRegisters[valC] = 1;
            } else
            {
                _copyOfRegisters[valC] = 0;
            }

            after = _copyOfRegisters;
        }
    }
}