using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNACalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNACalc.Tests {
    [TestClass()]
    public class SamplesTests {
        [TestMethod()]
        public void SamplesTest() {
            Util.CacheCombinations(4);
            Samples s = new Samples(new bool[][] {
                new bool[] { true, false, true, true },
                new bool[] { true, false, true, false },
                new bool[] { false, true, false, true },
                new bool[] { true, true, false, false },
            });

            Assert.AreEqual(0, s.Calc(1));
            Assert.AreEqual(3, s.Calc(2));
            Assert.AreEqual(4, s.Calc(3));
            Assert.AreEqual(1, s.Calc(4));
        }
    }
}