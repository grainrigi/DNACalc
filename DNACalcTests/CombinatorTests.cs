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

            int[][] combinations = new int[][] {
                new int[] { 0, 1, 2 },
                new int[] { 0, 1, 3 },
                new int[] { 0, 1, 4 },
                new int[] { 0, 2, 3 },
                new int[] { 0, 2, 4 },
                new int[] { 0, 3, 4 },
                new int[] { 1, 2, 3 },
                new int[] { 1, 2, 4 },
                new int[] { 1, 3, 4 },
            };

            foreach(var comb in combinations) {
                CollectionAssert.AreEqual(comb, c.combination);
                Assert.IsTrue(c.Next());
            }

            CollectionAssert.AreEqual(new int[] { 2, 3, 4 }, c.combination);
            Assert.IsFalse(c.Next());
        }

        [TestMethod()]
        public void SkipThisTest() {
            Util.CacheCombinations(5);
            Combinator c = new Combinator(5, 3);
            c.combination = new int[] { 0, 2, 3 };
            Assert.AreEqual(2, c.SkipThis(2));
            CollectionAssert.AreEqual(new int[] { 0, 2, 4 }, c.combination);
            c.combination = new int[] { 0, 1, 3 };
            Assert.AreEqual(5, c.SkipThis(1));
            CollectionAssert.AreEqual(new int[] { 0, 3, 4 }, c.combination);
        }
    }
}