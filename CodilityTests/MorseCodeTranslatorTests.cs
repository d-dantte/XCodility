using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class MorseCodeTranslatorTests
    {

        [TestMethod]
        public void Test()
        {
            var result = Solution("....");
            Assert.AreEqual("--..", result[0]);
            Assert.AreEqual(".--.", result[1]);
            Assert.AreEqual("..--", result[2]); 
            
            result = Solution(".");
            Assert.IsTrue(result.Count == 0);
        }


        public static IList<string> Solution(string morsecode)
        {
            var result = new List<string>();
            for (int cnt = 1; cnt < morsecode.Length; cnt++)
            {
                if (morsecode[cnt - 1] == '.' && morsecode[cnt] == '.')
                {
                    var newMorsecode =
                        morsecode.Substring(0, cnt - 1) +
                        "--" +
                        (cnt + 1 >= morsecode.Length ? "" : morsecode.Substring(cnt + 1));

                    result.Add(newMorsecode);
                }
            }

            return result;
        }
    }
}
