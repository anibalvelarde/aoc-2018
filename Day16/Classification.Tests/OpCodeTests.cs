using System;
using Classification.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Classification.Tests
{
    [TestClass]
    public class OpCodeTests
    {
        [TestMethod]
        public void should_execute_addi_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 7, 2 };
            var op = OpCodeFactory.MakeAddi(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 3, 9, 2 }, after);
        }

        [TestMethod]
        public void should_execute_addr_correctly()
        {
            // arrange
            var before = new int[] { 0, 2, 2, 3 };
            var op = OpCodeFactory.MakeAddr(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 2, 2, 4 }, after);
        }

        [TestMethod]
        public void should_execute_mulr_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 3, 3 };
            var op = OpCodeFactory.MakeMulr(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 3, 3, 9 }, after);
        }

        [TestMethod]
        public void should_execute_muli_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 3, 2 };
            var op = OpCodeFactory.MakeMuli(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 3, 6, 2 }, after);
        }

        [TestMethod]
        public void should_execute_borr_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 3, 1 };
            var op = OpCodeFactory.MakeBorr(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 1, 3, 1 }, after);
        }

        [TestMethod]
        public void should_execute_bori_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 3, 1 };
            var op = OpCodeFactory.MakeBori(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 3, 3, 1 }, after);
        }

        [TestMethod]
        public void should_execute_banr_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 3, 1 };
            var op = OpCodeFactory.MakeBanr(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 1, 3, 1 }, after);
        }

        [TestMethod]
        public void should_execute_bani_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 1, 3 };
            var op = OpCodeFactory.MakeBani(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 3, 1, 1 }, after);
        }

        [TestMethod]
        public void should_execute_setr_correctly()
        {
            // arrange
            var before = new int[] { 0, 2, 7, 3 };
            var op = OpCodeFactory.MakeSetr(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 2, 7, 7 }, after);
        }

        [TestMethod]
        public void should_execute_seti_correctly()
        {
            // arrange
            var before = new int[] { 0, 1, 7, 3 };
            var op = OpCodeFactory.MakeSeti(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 1, 7, 1 }, after);
        }

        [TestMethod]
        public void should_execute_gtir_correctly()
        {
            // arrange
            var before = new int[] { 0, 4, 3, 3 };
            var op = OpCodeFactory.MakeGtir(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 4, 3, 1 }, after);
        }

        [TestMethod]
        public void should_execute_gtri_correctly()
        {
            // arrange
            var before = new int[] { 15, 0, 8, 3 };
            var op = OpCodeFactory.MakeGtri(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 15, 0, 8, 1 }, after);
        }

        [TestMethod]
        public void should_execute_gtrr_correctly()
        {
            // arrange
            var before = new int[] { 15, 0, 3, 2 };
            var op = OpCodeFactory.MakeGtrr(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 15, 0, 1, 2 }, after);
        }

        [TestMethod]
        public void should_execute_eqir_correctly()
        {
            // arrange
            var before = new int[] { 7, 7, 0, 3 };
            var op = OpCodeFactory.MakeEqir(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 7, 7, 0, 1 }, after);
        }

        [TestMethod]
        public void should_execute_eqri_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 15, 3 };
            var op = OpCodeFactory.MakeEqri(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 3, 15, 0 }, after);
        }

        [TestMethod]
        public void should_execute_eqrr_correctly()
        {
            // arrange
            var before = new int[] { 0, 3, 3, 3 };
            var op = OpCodeFactory.MakeEqrr(before);

            // act 
            op.Execute(before, out var after);

            // assert
            CollectionAssert.AreEqual(new int[] { 0, 3, 3, 1 }, after);
        }
    }
}
