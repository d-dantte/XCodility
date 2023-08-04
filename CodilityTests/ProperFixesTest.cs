using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class ProperFixesTest
    {
        [TestMethod]
        public void Test()
        {
            var solution = new Codility.Challenges.PrefixSuffixMatch();

            Assert.AreEqual(0, solution.solution("codility"));
            Assert.AreEqual(4, solution.solution("abbabba"));
            Assert.AreEqual(8, solution.solution("bbbbbbbbb"));
        }
    }
}
