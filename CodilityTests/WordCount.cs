using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CodilityTests
{
    [TestClass]
    public class WordCount
    {
        public int Solution(string S)
        {
            var letterTable = new Dictionary<char, int>
            {
                ['B'] = 0,
                ['A'] = 0,
                ['L'] = 0,
                ['O'] = 0,
                ['N'] = 0
            };

            var singleLetters = new HashSet<char> { 'B', 'A', 'N' };
            var doubleLetters = new HashSet<char> { 'O', 'L' };

            foreach (var letter in S)
            {
                if (letterTable.ContainsKey(letter))
                {
                    letterTable[letter]++;
                }
            }

            return letterTable
                .Select(kvp => doubleLetters.Contains(kvp.Key)
                    ? kvp.Value / 2
                    : kvp.Value)
                .Min();
        }

        [TestMethod]
        public void Test()
        {
            var @string = "BAONXXOLL";
            var result = Solution(@string);
            Assert.AreEqual(1, result);

            @string = "BAOOLLNNOLOLGBAX";
            result = Solution(@string);
            Assert.AreEqual(2, result);

            @string = "QAWABAWONL";
            result = Solution(@string);
            Assert.AreEqual(0, result);

            @string = "ONLABLABLOON";
            result = Solution(@string);
            Assert.AreEqual(1, result);
        }
    }
}
