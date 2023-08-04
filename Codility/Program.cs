using System;
using System.Collections.Generic;

namespace Codility
{
    class Program
    {
        static void Main1(string[] args)
        {

            Console.WriteLine(Challenges.DataParser.MaxProfit(
                new[] { 2, 10, 8, 17 }));

            Console.WriteLine(Challenges.DataParser.MaxProfit(
                new[] { 1, 2, 3, 10 }));

            Console.WriteLine(Challenges.DataParser.MaxProfit(
                new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, }));

            Console.WriteLine(Challenges.DataParser.MaxProfit(
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, }));

            Console.WriteLine(Challenges.DataParser.MaxProfit(
                new[] { 4, 5, 5, 7, 5, 7, 6, 1, 200 }));

            Console.WriteLine(Challenges.DataParser.MaxProfit(
                new[] { 10, 12, 4, 5, 9 }));

            Console.WriteLine(Challenges.DataParser.MaxProfit(
                new[] { 14, 20, 4, 12, 5, 11 }));
        }

        static void Main2(string[] args)
        {
            Console.WriteLine(Challenges.ShortestSubset.Calculate(new List<long> { 2, 8, 10, 17 }, 3));
            Console.WriteLine(Challenges.ShortestSubset.Calculate(new List<long> { 3, 50, 100 }, 2));
        }

        public static void Main3(string[] args)
        {
            Challenges.MinimumRunningSum.Test();

            Console.Write("Press any key to exit: ");
            Console.ReadKey();
        }

        public static void Main4(string[] args)
        {
            Challenges.AppUserGrowth.Tests();
            Console.ReadKey();
        }

        public static void Main(string[] args)
        {
            var rotationCypher = new Challenges.RotationalCypher();

            var distance = 8;
            var testString = "the quick brown fox jumped over the lazy dog 357 times!";
            var cypherString = rotationCypher.Encrypt(testString, distance);
            Console.WriteLine(cypherString);
            var plainText = rotationCypher.Decrypt(cypherString, distance);
            Console.WriteLine(plainText);
            Console.WriteLine(plainText.Equals(testString));
        }
    }


}
