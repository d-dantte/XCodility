using Codility.Challenges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class BubbleSortTests
    {
        [TestMethod]
        public void TestBubbleSort()
        {
            var bubbleSort = new BubbleSort();

            var array = new[] { 3, 2, 1 };
            _ = bubbleSort.Sort(array, out var swapCount);
            Assert.AreEqual(3, swapCount);

            array = new[] { 2, 4, 3, 1, 6 };
            _ = bubbleSort.Sort(array, out swapCount);
            Assert.AreEqual(3, swapCount);

            array = new[] { 4, 11, 9, 10, 12 };
            _ = bubbleSort.Sort(array, out swapCount);
            Assert.AreEqual(0, swapCount);
        }
    }
}
