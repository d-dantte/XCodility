using System;

namespace Codility.Challenges
{
    public class MinimumRunningSum
    {

        /// <summary>
        /// Finds the minimum starting value for a running sum of an array that keeps the lowest sum equal to or greater a given value.
        /// 
        /// <para>
        /// Note that this is a generalized version of the actual question and it should work for any minimumStart and minimumSum value.
        /// </para>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="minimumStartValue"></param>
        /// <param name="minimumSumValue"></param>
        /// <returns></returns>
        public static long Solution(int[] array, int minimumStartValue, int minimumSumValue)
        {
            long sum = minimumStartValue;
            long finalStartValue = minimumStartValue;
            foreach (var value in array)
            {
                sum += value;

                if (sum < minimumSumValue)
                {
                    var difference = Math.Abs(minimumSumValue - sum);
                    sum += difference;
                    finalStartValue += difference;
                }
            }

            return finalStartValue;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="args"></param>
        public static void Test()
        {
            var array = new []{ 3, -6, 5, -2, 1};

            //to test always use 1 as minimum start and sum values.
            var result = Solution(array, 1, 1);
            Console.WriteLine($"Minimum start value for {array} is: {result}");

            array = new[] { -4, 3, 2, 1 };
            result = Solution(array, 1, 1);
            Console.WriteLine($"Minimum start value for {array} is: {result}");

            array = new[] { 5 };
            result = Solution(array, 1, 1);
            Console.WriteLine($"Minimum start value for {array} is: {result}");

            array = new[] { -2 };
            result = Solution(array, 1, 1);
            Console.WriteLine($"Minimum start value for {array} is: {result}");
        }
    }
}
