using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolymerReader.Lib;

namespace PolymerReader.Test
{
    [TestClass]
    public class PolymerReaderTests
    {
        [TestMethod]
        public void should_react_when_detecting_opposite_polarity_for_2_units()
        {
            // arrange
            string polymer = "aA";
            var pr = new PolymerReader.Lib.PolymerReader(polymer);
            
            // act
            string reactedPolymer = pr.Trigger();

            // assert
            Assert.AreEqual("", reactedPolymer);
        }

        [TestMethod]
        public void should_react_when_detecting_opposite_polarity_to_destroy_itself()
        {
            // arrange
            string polymer = "abBA";
            var pr = new PolymerReader.Lib.PolymerReader(polymer);

            // act
            string reactedPolymer = pr.Trigger();

            // assert
            Assert.AreEqual("", reactedPolymer);
        }

        [TestMethod]
        public void should_not_react_when_polarities_are_ok()
        {
            // arrange 
            string polymer = "abAB";
            var pr = new PolymerReader.Lib.PolymerReader(polymer);

            // act
            string reactedPolymer = pr.Trigger();

            // assert
            Assert.AreEqual(polymer, reactedPolymer);
        }

        [TestMethod]
        public void should_not_react_when_polarities_are_ok_on_larger_polymers()
        {
            // arrange 
            string polymer = "aabAAB";
            var pr = new PolymerReader.Lib.PolymerReader(polymer);

            // act
            string reactedPolymer = pr.Trigger();

            // assert
            Assert.AreEqual(polymer, reactedPolymer);
        }

        [TestMethod]
        public void should_reduce_polymer_correctly()
        {
            // arrange 
            string polymer = "dabAcCaCBAcCcaDA"; var expPolymer = "dabCBAcaDA";
            var pr = new PolymerReader.Lib.PolymerReader(polymer);

            // act
            string reactedPolymer = pr.Trigger();

            // assert
            Assert.AreEqual(expPolymer, reactedPolymer);
        }
    }
}
