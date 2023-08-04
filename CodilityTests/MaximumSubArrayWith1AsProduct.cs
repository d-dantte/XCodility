using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class MaximumSubArrayWith1AsProduct
    {
        [TestMethod]
        public void Test()
        {
            var values = new[] { 1, -1, -1,-1, 1, 1};
            var result = MaxSubArrayLength(values.ToList());
            Assert.AreEqual(4, result);
        }


        public int MaxSubArrayLength(List<int> badges)
        {
            var negatives = badges
                .Select((value, index) => (value, index))
                .Where(tuple => tuple.value == -1)
                .Select(tuple => tuple.index)
                .ToArray();

            if(isZeroOrEven(negatives.Length))
            {
                return badges.Count;
            }    
            else
            {
                //left distance - number of elements before the first negative
                var leftDistance = negatives[0];
                var rightDistance = badges.Count - 1 - negatives.Last();

                if(leftDistance >= rightDistance)
                {
                    return negatives.Last();
                }
                else
                {
                    return badges.Count - (negatives[0] + 1);
                }
            }
        }

        private bool isZeroOrEven(int value) => value == 0 || value % 2 == 0;
    }
}
