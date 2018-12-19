namespace Classification.Lib
{
    public class OpAddi : Operator
    {
        public OpAddi(int[] initState)
            :base(initState)
        {
            opId = "addi";
            OpCode = 1;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA] + valB;

            after = _copyOfRegisters;
        }
    }
}
