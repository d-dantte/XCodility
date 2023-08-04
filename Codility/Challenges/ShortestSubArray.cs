using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class ShortestSubArray
    {
        public static int Solution(int[] slots, int minimumSlots)
        {
            if(slots.Length == 1)
            {
                return 1;
            }

            var sortedSlots = slots
                .OrderBy(slot => slot)
                .ToArray();

            if(minimumSlots >= (sortedSlots[sortedSlots.Length-1] - sortedSlots[0]))
            {
                return minimumSlots;
            }

            //accumulate the consecutive distances
            var distances = new List<int>();
            for (int cnt = 1; cnt < sortedSlots.Length; cnt++)
            {
                distances.Add(sortedSlots[cnt] - sortedSlots[cnt - 1]);
            }

            //set up the sliding window
            int lboundary = 0, rboundary = 0;
            int currentSum = 1, minSum = 1;
            for (; rboundary < distances.Count; rboundary++)
            {
                if(rboundary < minimumSlots-1)
                {
                    minSum = (currentSum += distances[rboundary]);
                }
                else
                {
                    currentSum -= distances[lboundary];
                    currentSum += distances[rboundary];
                    lboundary++;

                    if(currentSum < minSum)
                    {
                        minSum = currentSum;
                    }
                }
            }

            return minSum;
        }
    }
}
