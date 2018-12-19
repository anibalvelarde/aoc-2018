namespace Classification.Lib
{
    public class OpEqir : Operator
    {
        public OpEqir(int[] initState)
            : base(initState)
        {
            opId = "opEqir";
            OpCode = 0;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            if (valA == _copyOfRegisters[valB])
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