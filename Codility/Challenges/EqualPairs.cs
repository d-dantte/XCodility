using System.Collections.Generic;
using System.Linq;

namespace Codility.EqualPairs
{
    public class EqualPairs
    {
        public bool Solution(int[] A)
        {
            if (A == null || A.Length == 0)
                return false;

            //Create a map to hold values and their count within the source array
            var countMap = new Dictionary<int, int>();

            //loop through the array, keeping a count of how often each value appears
            foreach(var a in A)
            {
                if (!countMap.ContainsKey(a))
                    countMap[a] = 1;

                else
                {
                    //increment count
                    countMap[a] = countMap[a] + 1;
                }
            }

            //make sure all values in the array occured an even number of times
            return countMap
                .Values
                .All(IsEven);
        }

        //determine if a value is even
        public bool IsEven(int value) => value % 2 == 0;
    }
}
