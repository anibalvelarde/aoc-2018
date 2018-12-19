using System;
using System.Collections.Generic;
using Classification.Lib;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Classification.Tests
{
    [TestClass]
    public class PuzzleOneTests
    {
        private List<KeyValuePair<string, Operator>> _sameResultOp = new List<KeyValuePair<string, Operator>>();

        [TestMethod]
        public void should_get_same_results_as_day_16_example()
        {
            int[] before = { 3, 2, 1, 1 };
            int[] sample = { 9, 2, 1, 2 };
            int[] after = { 3, 2, 2, 1 };

            var cpu = new Cpu(before);
            foreach (var op in cpu.Operations)
            {
                var answer = cpu.TryExecuteInstruction(MakeCopyOfCurrentRegisters(before), sample, op.Value);
                if (answer.SequenceEqual(after)) _sameResultOp.Add(op);
            }

            Assert.AreEqual(3, _sameResultOp.Count);
        }

        [TestMethod]
        public void should_process_sample_data_correctly()
        {
            // Arrange 
            var beforeData = @"Before: [0, 1, 2, 1]";
            var instructionData = @"14 1 3 3";
            var afterData = @"After:  [0, 1, 2, 1]";

            // Act
            var s = new Sample(beforeData, instructionData, afterData);

            // Assert
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 1 }, s.RegisterBefore);
            CollectionAssert.AreEqual(new int[] { 14, 1, 3, 3 }, s.Instruction);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 1 }, s.RegisterAfter);
        }

        private int[] MakeCopyOfCurrentRegisters(int[] registers)
        {
            int[] copyOfRegisters = { registers[0], registers[1], registers[2], registers[3] };
            return copyOfRegisters;
        }
    }
}
