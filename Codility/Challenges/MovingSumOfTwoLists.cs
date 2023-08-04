using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    public class MovingSumOfTwoLists
    {
        public static int findMaximumSustainableClusterSize(
            List<int> processingPower,
            List<int> bootingPower,
            long powerMax)
        {
            int limit = processingPower.Count;
            var maxClusterSize = 0;
            for(int start = 0, end = 0; end < limit; end++)
            {
                var clusterSize = (end - start) + 1;
                var powerSum = 
                    processingPower.Skip(start).Take(clusterSize).Sum() 
                    + bootingPower.Skip(start).Take(clusterSize).Max();

                if (powerSum > powerMax)
                {
                    start++;
                }
                else
                {
                    if (maxClusterSize < clusterSize)
                    {
                        maxClusterSize = clusterSize;
                    }

                    if (powerSum == powerMax)
                    {
                        start++;
                    }
                }
            }


            return maxClusterSize;
        }
    }
}
