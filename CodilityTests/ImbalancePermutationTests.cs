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
    public class ImbalancePermutationTests
    {
        [TestMethod]
        public void TestBubbleSort()
        {
            var imbalances = new PermutationImbalances();

            var array = new[] { 4, 1, 3, 2 };
            var count = imbalances.FindAllImbalances(array);
            Assert.AreEqual(3, count);

            array = new[] { 1, 2 };
            count = imbalances.FindAllImbalances(array);
            Assert.AreEqual(0, count);
        }
    }
}
