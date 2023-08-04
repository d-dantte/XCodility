using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    public class FiarIndexChallenge
    {
        public int solution(int[] A, int[] B)
        {
            int leftSumA = 0,
                rightSumA = A.Sum();
            int leftSumB = 0,
                rightSumB = B.Sum();

            int fairIndexSum = 0;
            for(int index = 0; index < A.Length; index++)
            {
                leftSumA += A[index];
                leftSumB += B[index];
                rightSumA -= A[index];
                rightSumB -= B[index];

                if (leftSumA == leftSumB
                    && rightSumA == rightSumB
                    && leftSumA == rightSumA)
                {
                    fairIndexSum++;
                }
            }

            return fairIndexSum;
        }
    }
}
