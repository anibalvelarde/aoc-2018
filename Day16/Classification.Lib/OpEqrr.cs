namespace Classification.Lib
{
    public class OpEqrr : Operator
    {
        public OpEqrr(int[] initState)
            : base(initState)
        {
            opId = "opEqrr";
            OpCode = 11;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            if (_copyOfRegisters[valA] == _copyOfRegisters[valB])
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