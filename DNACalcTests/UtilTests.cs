using Microsoft.VisualStudio.TestTools.UnitTesting;
using DNACalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNACalc.Tests {
    [TestClass()]
    public class UtilTests {
        [TestMethod()]
        public void bs2ulongTest() {
            Assert.AreEqual((ulong)0b100101, Util.bs2ulong(new bool[] { true, false, false, true, false, true }, 6));
            Assert.AreEqual((ulong)0b10010100, Util.bs2ulong(new bool[] { true, false, false, true, false, true }, 8));
        }
    }
}