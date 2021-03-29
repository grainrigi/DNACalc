using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNACalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNACalc.Tests {
    [TestClass()]
    public class CombinatorTests {
        [TestMethod()]
        public void CombinatorTest() {
            Combinator c = new Combinator(5, 3);

            byte[][] combinations = new byte[][] {
                new byte[] { 0, 1, 2 },
                new byte[] { 0, 1, 3 },
                new byte[] { 0, 1, 4 },
                new byte[] { 0, 2, 3 },
                new byte[] { 0, 2, 4 },
                new byte[] { 0, 3, 4 },
                new byte[] { 1, 2, 3 },
                new byte[] { 1, 2, 4 },
                new byte[] { 1, 3, 4 },
            };

            foreach(var comb in combinations) {
                CollectionAssert.AreEqual(comb, c.combination);
                Assert.IsTrue(c.Next());
            }

            CollectionAssert.AreEqual(new byte[] { 2, 3, 4 }, c.combination);
            Assert.IsFalse(c.Next());
        }

        [TestMethod()]
        public void SkipThisTest() {
            Combinator c = new Combinator(5, 3);
            Assert.AreEqual(1, c.SkipThis(new byte[] { 0, 2, 3, 4 }, 3));
            CollectionAssert.AreEqual(new byte[] { 0, 2, 3 }, c.combination);
            Assert.AreEqual(5, c.SkipThis(new byte[] { 0, 1, 3, 4 }, 1));
            CollectionAssert.AreEqual(new byte[] { 0, 3, 4 }, c.combination);
        }
    }
}