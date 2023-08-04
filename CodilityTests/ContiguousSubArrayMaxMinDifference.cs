using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class ContiguousSubArrayMaxMinDifference
    {
        [TestMethod]
        public void Test()
        {
            var value = new int[] { 3, 2, 3};
            var result = getTotalImbalance(value.ToList());
        }


        public static long getTotalImbalance(List<int> weight)
        {
            var sum = 0;
            for(int findex = 0; findex < weight.Count; findex++)
            {
                int? max = null;
                int? min = null;
                for (int sindex = findex+1; sindex < weight.Count; sindex++)
                {
                    if(max == null)
                    {
                        max = Math.Max(weight[findex], weight[sindex]);
                        min = Math.Min(weight[findex], weight[sindex]);
                    }
                    else
                    {
                        max = Math.Max(max.Value, weight[sindex]);
                        min = Math.Min(min.Value, weight[sindex]);
                    }

                    sum += max.Value - min.Value;
                }
            }

            return sum;
        }
    }
}
