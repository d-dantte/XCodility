using System;
using System.Collections.Generic;

namespace Codility.Challenges
{
    public class MinimumCoinFlips
    {
        public static int Solution(int[] A)
        {
            var target1 = new List<int> { 1 };
            var target2 = new List<int> { 0 };

            for(int cnt = 1; cnt < A.Length; cnt++)
            {
                AppendFlip(target1);
                AppendFlip(target2);
            }

            return Math.Min(
                Difference(target1, A),
                Difference(target2, A));
        }

        public static void AppendFlip(List<int> list)
        {
            if (list[list.Count - 1] == 0)
                list.Add(1);

            else list.Add(0);
        }

        public static int Difference(List<int> list, int[] arr)
        {
            int difference = 0;
            for (int cnt = 0; cnt < list.Count; cnt++)
            {
                if (list[cnt] != arr[cnt])
                    difference++;
            }

            return difference;
        }
    }
}
