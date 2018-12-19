namespace Classification.Lib
{
    public class OpGtir : Operator
    {
        public OpGtir(int[] initState)
            : base(initState)
        {
            opId = "opGtir";
            OpCode = 2;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            if (valA > _copyOfRegisters[valB])
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