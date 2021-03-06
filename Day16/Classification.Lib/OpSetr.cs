﻿namespace Classification.Lib
{
    public class OpSetr : Operator
    {
        public OpSetr(int[] initState)
            : base(initState)
        {
            opId = "setr";
            OpCode = 3;
        }

        public override void Execute(int[] instruction, out int[] after)
        {
            base.Execute(instruction);

            _copyOfRegisters[valC] = _copyOfRegisters[valA];

            after = _copyOfRegisters;
        }
    }
}
