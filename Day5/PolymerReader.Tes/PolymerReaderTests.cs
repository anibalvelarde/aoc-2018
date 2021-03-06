﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolymerReader.Lib;

namespace PolymerReader.Test
{
    [TestClass]
    public class PolymerReaderTests
    {
        private string[] testPolymersThatCollapseToNothing =
        {
            "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ",
            "abcdefghijklmnopqrstuvwxyzZYXWVUTSRQPONMLKJIHGFEDCBA",
            "abBA",
            "aA", "bB", "cC", "dD", "eE", "fF", "gG", "hH", "iI", "jJ", "kK", "lL",
            "mM", "nN", "oO", "pP", "qQ", "rR", "sS", "tT", "uU", "vV", "xX", "yY", "zZ"
        };

        private string[] testPolymersThatAreNotCollapsable =
        {
            "abAB",
            "aabAAB",
            "abcdefghijklmnopqrstuvwxyz",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        };

        [TestMethod]
        public void should_collapse_to_nothing()
        {
            foreach (var polymer in testPolymersThatCollapseToNothing)
            {
                // arrange
                var revPolymer = ReverseString(polymer);
                var pr = new PolymerReader.Lib.PolymerReader(polymer);
                var prRev = new PolymerReader.Lib.PolymerReader(revPolymer);

                // act
                string reactedPolymer = pr.Trigger();
                string reactedPolymerRev = prRev.Trigger();

                // assert
                Assert.AreEqual("", reactedPolymer);
                Assert.AreEqual("", reactedPolymerRev);
            }
        }

        [TestMethod]
        public void should_remain_the_same()
        {
            foreach (var polymer in testPolymersThatAreNotCollapsable)
            {
                // arrange
                var revPolymer = ReverseString(polymer);
                var pr = new PolymerReader.Lib.PolymerReader(polymer);
                var prRev = new PolymerReader.Lib.PolymerReader(revPolymer);

                // act
                string reactedPolymer = pr.Trigger();
                string reactedPolymerRev = prRev.Trigger();

                // assert
                Assert.AreEqual(polymer, reactedPolymer);
                Assert.AreEqual(revPolymer, reactedPolymerRev);
            }
        }

        [TestMethod]
        public void should_collapse_polymer_correctly_to_some_units()
        {
            // arrange 
            string polymer = "pabcdefghijklmnopqrstuvwxyzZYXWVUTSRQPONMLKJIHGFEDCBAp";
            var expPolymer = "pp";
            string polymerReversed = ReverseString(polymer);
            var pr = new PolymerReader.Lib.PolymerReader(polymer);
            var prRev = new PolymerReader.Lib.PolymerReader(polymerReversed);

            // act
            string reactedPolymer1 = pr.Trigger();
            string reactedPolymer2 = prRev.Trigger();

            // assert
            Assert.AreEqual(expPolymer, reactedPolymer1);
            Assert.AreEqual(expPolymer, reactedPolymer2);
        }

        private string ReverseString(string polymer)
        {
            var a = polymer.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }

        [TestMethod]
        public void should_reduce_polymer_correctly()
        {
            // arrange        dabAcCaCBAcCcaDA
            string polymer = "dabAcCaCBAcCcaDA"; var expPolymer = "dabCBAcaDA";
            var pr = new PolymerReader.Lib.PolymerReader(polymer);

            // act
            string reactedPolymer = pr.Trigger();

            // assert
            Assert.AreEqual(expPolymer, reactedPolymer);
        }

        [TestMethod]
        public void should_find_unit_to_remove_to_optimize_collapse_of_polymer()
        {
            var toRemove = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var polymer = "dabAcCaCBAcCcaDA";
            var reduxResult = new Dictionary<char, int>();

            foreach (char removeThis in toRemove)
            {
                var pr = new PolymerReader.Lib.PolymerReader(polymer, removeThis);
                var reactedPolymer = pr.Trigger();

                reduxResult.Add(removeThis, reactedPolymer.Length);
            }

            KeyValuePair<char,int> seedKey = new KeyValuePair<char, int>('\0',int.MaxValue);
            var optimal = reduxResult.Aggregate(seedKey, (optimalObs, nextObs) =>
            {
                return nextObs.Value < optimalObs.Value ? nextObs : optimalObs;
            });
            Console.WriteLine($"The key [{optimal.Key}] yields the smallest polymer units at [{optimal.Value}].");
        }
    }
}
