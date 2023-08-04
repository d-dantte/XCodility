using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    [TestClass]
    public class FiarIndexChallenge
    {
        [TestMethod]
        public void Test()
        {
            int[] TestA = new[] { 2, -2, -3, 3 };
            int[] TestB = new[] { 0, 0, 4, -4 };

            Console.WriteLine(solution(TestA, TestB));

        }


        public static int solution(int[] A, int[] B)
        {
            int leftSumA = 0,
                rightSumA = A.Sum();
            int leftSumB = 0,
                rightSumB = B.Sum();

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
                    return index;
                }
            }

            return -1;
        }
    }
}
