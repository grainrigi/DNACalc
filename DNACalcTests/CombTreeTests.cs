using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNACalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNACalc.Tests {
    [TestClass()]
    public class CombTreeTests {
        [TestMethod()]
        public void CombTreeTest() {
            CombTree t = new CombTree();
            Assert.AreEqual(1, t.Search(new byte[] { 0 }));
            t.AddCombination(new byte[] { 0 });
            t.AddCombination(new byte[] { 1 });
            t.AddCombination(new byte[] { 2 });
            t.AddCombination(new byte[] { 4 });
            Assert.AreEqual(2, t.Search(new byte[] { 0, 1 }));
            Assert.AreEqual(1, t.Search(new byte[] { 3, 4 }));
            t.AddCombination(new byte[] { 0, 1 });
            t.AddCombination(new byte[] { 0, 4 });
            Assert.AreEqual(3, t.Search(new byte[] { 0, 1, 2 }));
            Assert.AreEqual(2, t.Search(new byte[] { 0, 2, 3 }));
        }
    }
}