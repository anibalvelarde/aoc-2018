namespace Classification.Lib
{
    public class OpGtrr : Operator
    {
        public OpGtrr(int[] initState)
            : base(initState)
        {
            opId = "opGtrr";
            OpCode = 10;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            if (_copyOfRegisters[valA] > _copyOfRegisters[valB])
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