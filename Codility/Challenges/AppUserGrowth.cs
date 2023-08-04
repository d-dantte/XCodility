using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class AppUserGrowth
    {
        public static int GetBillionUsersDay(float[] growthRates)
        {
            // first group the growth rates
            var groupedRates = growthRates
                .GroupBy(g => g)
                .Select(group => new KeyValuePair<float, int>(group.Key, group.Count()))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // find the power that raises the largest value to a billion
            var largestRate = groupedRates.Keys.Max();
            var largestPower = (int) Math.Ceiling(Math.Log(1000000000, largestRate)); //Ceiling ensures that the whole number power will yield a value greater than 1 billion.

            var billionthUserDay = largestPower;
            while (GrowthSum(groupedRates, billionthUserDay) > 1000000000d)
                billionthUserDay--;

            return billionthUserDay + 1;
        }

        private static double GrowthSum(Dictionary<float, int> groupedRates, int power)
        {
            return groupedRates.Aggregate(0d, (sum, rate) =>
            {
                return sum + (rate.Value * Math.Pow(rate.Key, power));
            });
        }

        private static string PrintArray(float[] array)
        {
            var @string = $"[{String.Join(", ", array.Take(10).Select(v => v.ToString()))}";

            if (array.Length <= 10)
                return @string + "]";

            else
                return @string + ",...]";
        }


        public static void Tests()
        {
            var array = new[] { 1.5f };
            int result = GetBillionUsersDay(array);
            Console.WriteLine($"Result for: {PrintArray(array)} is: {result}\n");


            array = new[] { 1.1f, 1.2f, 1.3f };
            result = GetBillionUsersDay(array);
            Console.WriteLine($"Result for: {PrintArray(array)} is: {result}\n");


            array = new[] { 1.01f, 1.02f };
            result = GetBillionUsersDay(array);
            Console.WriteLine($"Result for: {PrintArray(array)} is: {result}\n");
        }
    }
}
