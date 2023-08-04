using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CodilityTests
{
    [TestClass]
    public class FileSizeEstimate
    {
        public int solution(int X, int[] B, int Z)
        {
            // file is empty
            if (X <= 0)
                return 0;

            // download is complete
            if (B.Sum() >= X)
                return 0;

            // calculate average
            var count = Math.Min(Z, B.Length);
            var average = B.Skip(B.Length - count).Sum() / (double)count;

            // calculate remaining bytes
            var remainingBytes = X - B.Sum();

            // estimate
            return (int)Math.Ceiling(remainingBytes / average);
        }


        [TestMethod]
        public void Test1()
        {
            var result = solution(100, new[] { 10, 6, 6, 8 }, 2);
            Assert.AreEqual(10, result);


            result = solution(10, new[] { 2, 3 }, 2);
            Assert.AreEqual(2, result);
        }
    }
}
