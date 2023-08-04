using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class MiscTests
    {
        private static readonly Regex VariablePattern = new Regex("\\$.+\\b");


        [TestMethod]
        public void Tests()
        {
            var file = new FileInfo("C:\\Dantte\\Dev\\Repos\\Test-Files\\template.html");
            using var reader = new StreamReader(file.OpenRead());
            var text = reader.ReadToEnd();

        }

        [TestMethod]
        public void TestMethod()
        {
            //var value = maxElement("abcdd");
            //Assert.AreEqual("d", value);

            //value = maxElement("puff");
            //Assert.AreEqual("f", value);

            //value = maxElement("a string");
            //Assert.AreEqual("0", value);

            var someString = "fdkfj;lk ka;dlfjfadjfkeufhfjs kejbc kgir3thieor2;iqwdjm ndeoetkrhewjwjdcvikreuldjn ";
            var groups = someString
                .GroupBy(c => c)
                .ToArray();
            foreach(var group in groups)
            {
                foreach (var _char in group)
                    Console.WriteLine(_char);
            }

            var dictionary = new Dictionary<char, List<char>>();
            foreach (var character in someString)
            {
                if (!dictionary.ContainsKey(character))
                {
                    dictionary.Add(character, new List<char>());
                }

                dictionary[character].Add(character);
            }
        }

        public string maxElement(string input1)
        {
            var charMap = new Dictionary<char, int>();
            foreach(var c in input1)
            {
                if (charMap.ContainsKey(c))
                    charMap[c]++;

                else charMap[c] = 1;
            }

            var sorted = charMap
                .OrderByDescending(v => v.Value)
                .ToArray();

            if (sorted.Length == 0)
                return "0";

            if (sorted.Length == 1)
                return sorted[0].Key.ToString();

            else if (sorted[0].Value == sorted[1].Value)
                return "0";

            else return sorted[0].Key.ToString();
        }

        public int sum(int input11, int[] input2)
        {
            // 1,1,3,1,1,4,5,5,6,6,2,3,3,6,6,6,6
            var values = input2
                .GroupBy(i => i)
                .OrderBy(g => g.Key)
                .ToArray();

            if (values.Length == 0)
                return -1;

            else if (values.Length == 1)
                return values[0].Key;

            else 
                return values[0].Key + values[values.Length - 1].Key;
        }

       
    }

    public class Person
    {
        string Name { get; set; }
        public string Country { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
    }
}
