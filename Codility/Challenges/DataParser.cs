using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Codility.Challenges
{
    public class DataParser
    {

        private static readonly Regex AgePattern = new Regex("age\\=\\d+");
        public int DataAgeCounter(string data)
        {
            return AgePattern
                .Matches(data)
                .Select(match => match.Value.Replace("age=", ""))
                .Select(int.Parse)
                .Count(age => age >= 50);
        }


        private static readonly Regex SerialPattern = new Regex("^\\d{3}\\.\\d{3}\\.\\d{3}$");
        public bool IsSerialNumber(string value)
        {
            if (!SerialPattern.IsMatch(value))
                return false;

            var sets = value.Split('.');
            if (!IsEven(SetSum(sets[0]))
                || !IsOdd(SetSum(sets[1])))
                return false;

            if (!sets.All(LastDigitGreater))
                return false;

            return true;
        }

        private bool IsEven(int value) => value % 2 == 0;
        private bool IsOdd(int value) => !IsEven(value);
        private int SetSum(string set) => set.Select(AsInt).Sum();
        private int AsInt(char @char) => int.Parse(@char.ToString());
        private bool LastDigitGreater(string digits)
        {
            var ints = digits.Select(AsInt).ToArray();
            return ints[2] > ints[0]
                && ints[2] > ints[1];
        }

        public static int MaxProfit(int[] arr)
        {
            int? lowest = null;
            int? profit = null;

            foreach(var price in arr)
            {
                if (lowest == null || price < lowest == true)
                    lowest = price;

                else
                {
                    var _profit = price - lowest.Value;
                    if (_profit < 0)
                        lowest = price;

                    else if (profit == null || _profit > profit == true)
                        profit = _profit;
                }
            }

            return profit ?? -1;
        }
    }
}
