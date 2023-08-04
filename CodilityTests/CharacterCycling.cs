using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class CharacterCycling
    {

        [TestMethod]
        public void Tests()
        {
            var count = countFamilyLogins(new List<string>
            {
                "bag",
                "sfe",
                "cbh",
                "cbh",
                "red"
            });
            Assert.AreEqual(3, count);

            count = countFamilyLogins(new List<string>
            {
                "corn", //"dpso"
                "horn", //"ipso"
                "dpso", //"eqtp"
                "eqtp", //"fruq"
                "corn"  
            });
            Assert.AreEqual(3, count);
        }

        public static int countFamilyLogins(List<string> logins)
        {
            var rotated = logins
                .Select(Rotate)
                .GroupBy(@string => @string)
                .ToDictionary(group => group.Key, group => group.Count());

            return logins
                .Where(rotated.ContainsKey)
                .Select(login => rotated[login])
                .Sum();
        }

        private static string Rotate(string @string)
        {
            var charArray = @string
                .Select(chr => chr - 97)
                .Select(chr => (chr + 1) % 26)
                .Select(chr => (char)(chr+ 97))
                .ToArray();

            return new string(charArray);
        }
    }
}
