using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolymerReader.Lib;

namespace PolymerReader.Test
{
    [TestClass]
    public class PolymerReaderTests
    {
        [TestMethod]
        public void should_react_when_detecting_opposite_polarity()
        {
            // arrange
            string polymer = "aA";
            var pr = new PolymerReader.Lib.PolymerReader(polymer);
            
            // act
            string reactedPolymer = pr.React();

            // assert
            Assert.AreEqual("", reactedPolymer);
        }
    }
}
