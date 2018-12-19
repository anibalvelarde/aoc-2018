namespace Classification.Lib
{
    public class OpGtri : Operator
    {
        public OpGtri(int[] initState)
            : base(initState)
        {
            opId = "opGtri";
            OpCode = 13;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            if (_copyOfRegisters[valA] > valB)
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