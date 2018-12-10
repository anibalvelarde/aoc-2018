using System;
using System.IO;
using DirectedGraph.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DirectedGraph.Test
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void should_read_data_correctly_for_single_statement()
        {
            // Arange
            var statement = "Step C must be finished before step A can begin.";

            // Act
            var g = new Graph();
            g.AddStatement(statement);

            // Assert
            Assert.IsNotNull(g.Vertices['C']);
            Assert.IsNotNull(g.Vertices['A']);
            Assert.AreEqual("CA", g.GetPrecedenceSequence());
        }

        [TestMethod]
        public void should_calc_precedence_correctly_for_single_statement()
        {
            // Arange
            var statement = "Step C must be finished before step A can begin.";

            // Act
            var g = new Graph();
            g.AddStatement(statement);

            // Assert
            Assert.AreEqual("CA", g.GetPrecedenceSequence());
        }

        [TestMethod]
        public void should_calc_precedence_correctly_for_two_statement()
        {
            // Arange
            var statement = "Step C must be finished before step A can begin.";
            var statement2 = "Step C must be finished before step F can begin.";

            // Act
            var g = new Graph();
            g.AddStatement(statement);
            g.AddStatement(statement2);

            // Assert
            Assert.AreEqual("CAF", g.GetPrecedenceSequence());
        }

        [TestMethod]
        public void should_calc_precedence_correctly_for_three_statement()
        {
            // Arange
            var statement = "Step D must be finished before step A can begin.";
            var statement2 = "Step D must be finished before step B can begin.";
            var statement3 = "Step D must be finished before step C can begin.";

            // Act
            var g = new Graph();
            g.AddStatement(statement);
            g.AddStatement(statement2);
            g.AddStatement(statement3);

            // Assert
            Assert.AreEqual("DABC", g.GetPrecedenceSequence());
        }

        [TestMethod]
        public void should_read_data_correctly_for_multiple_statement()
        {
            // Arange
            var statements = File.ReadAllLines(@"data\TestData.txt");
            var g = new Graph();

            foreach (var statement in statements)
            {
                // Act
                g.AddStatement(statement);
            }

            // Assert
            Assert.IsNotNull(g.Vertices['A']);
            Assert.IsNotNull(g.Vertices['B']);
            Assert.IsNotNull(g.Vertices['C']);
            Assert.IsNotNull(g.Vertices['D']);
            Assert.IsNotNull(g.Vertices['E']);
            Assert.IsNotNull(g.Vertices['F']);

            Assert.AreEqual("CABDFE", g.GetPrecedenceSequence());
        }

        [TestMethod]
        public void should_read_data_correctly_for_multiple_statement_v2()
        {
            // Arange
            var statements = File.ReadAllLines(@"data\TestData2.txt");
            var g = new Graph();

            foreach (var statement in statements)
            {
                // Act
                g.AddStatement(statement);
            }

            // Assert

            Assert.AreEqual("CABDFE", g.GetPrecedenceSequence());
        }
    }
}
