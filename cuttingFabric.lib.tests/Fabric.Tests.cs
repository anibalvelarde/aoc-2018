using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cuttingFabric.lib.tests
{
    [TestClass]
    public class FabricTests
    {
        [TestMethod]
        public void should_create_10_x_10_fabric()
        {
            // arange
            var l = 10;

            // act
            var f = new Fabric(l);

            // assert
            Assert.AreEqual(100, f.SquareInches);
        }

        [TestMethod]
        public void should_get_0_overlaping_sqin_when_none_overlap()
        {
            // arange
            var f = new Fabric(2);
            var c1 = new Claim("#1 @ 0,0: 1x1");
            var c2 = new Claim("#2 @ 1,1: 1x1");

            // act
            f.AddClaim(c1);
            f.AddClaim(c2);

            // assert
            Assert.AreEqual(0, f.CalculateOverlapingSquareInches());
        }

        [TestMethod]
        public void should_get_1_overlaping_sqin_when_all_3_overlap()
        {
            // arange
            var f = new Fabric(3);
            var c1 = new Claim("#1 @ 0,0: 2x2");
            var c2 = new Claim("#2 @ 1,1: 2x2");
            var c3 = new Claim("#3 @ 0,1: 2x2");

            // act
            f.AddClaim(c1);
            f.AddClaim(c2);
            f.AddClaim(c3);

            // assert
            Assert.AreEqual(3, f.CalculateOverlapingSquareInches());
        }

        [TestMethod]
        public void should_get_4_overlaping_sqin_when_all_3_fabric_claims()
        {
            // arange
            var f = new Fabric(8);
            var c1 = new Claim("#1 @ 1,3: 4x4");
            var c2 = new Claim("#2 @ 3,1: 4x4");
            var c3 = new Claim("#3 @ 5,5: 3x2");

            // act
            f.AddClaim(c1);
            f.AddClaim(c2);
            f.AddClaim(c3);

            // assert
            Assert.AreEqual(4, f.CalculateOverlapingSquareInches());
        }

        [TestMethod]
        public void should_find_the_claim_that_does_not_overlap()
        {
            // arange
            var f = new Fabric(8);
            var c1 = new Claim("#1 @ 1,3: 4x4");
            var c2 = new Claim("#2 @ 3,1: 4x4");
            var c3 = new Claim("#3 @ 5,5: 3x2");

            // act
            f.AddClaim(c1);
            f.AddClaim(c2);
            f.AddClaim(c3);

            // assert
            Assert.IsFalse(f.DoesItOverlap(c3));
            Assert.IsTrue(f.DoesItOverlap(c1));
            Assert.IsTrue(f.DoesItOverlap(c2));
        }
    }
}
