using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodilityTests
{
    [TestClass]
    public class StringComparisonTest
    {
        [TestMethod]
        public void Test()
        {
            var comparison = new Codility.Challenges.StringComparer();

            Assert.IsTrue(comparison.Solution("2pl1", "a2le"));
            Assert.IsTrue(comparison.Solution("a10", "10a"));
            Assert.IsFalse(comparison.Solution("ba1", "1Ad"));
            Assert.IsFalse(comparison.Solution("3x2x", "8"));
        }
    }
}
