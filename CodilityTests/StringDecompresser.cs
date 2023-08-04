using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace CodilityTests
{
    [TestClass]
    public class StringDecompresser
    {
        public static string Decompress(string input)
        {
            var decompressed = new StringBuilder();
            var sbuffer = new StringBuilder();
            var dbuffer = new StringBuilder();
            foreach(var c in input)
            {
                if (dbuffer.Length == 0)
                {
                    if (char.IsLetter(c))
                        sbuffer.Append(c);

                    else
                        dbuffer.Append(c);
                }
                else
                {
                    if (char.IsDigit(c))
                        dbuffer.Append(c);

                    else
                    {
                        decompressed.Append(Decompress(sbuffer.ToString(), int.Parse(dbuffer.ToString())));
                        dbuffer.Clear();
                        sbuffer.Clear();
                        sbuffer.Append(c);
                    }
                }
            }

            decompressed.Append(
                Decompress(
                    sbuffer.ToString(),
                    int.Parse(dbuffer.Length == 0 ? "0": dbuffer.ToString())));

            return decompressed.ToString();
        }

        public static string Decompress(string s, int n)
        {
            var buffer = new StringBuilder();
            for(int cnt=0; cnt < n; cnt++)
            {
                buffer.Append(s);
            }
            return buffer.ToString();
        }

        [TestMethod]
        public void Tests()
        {
            var decompressed = Decompress("cv3b2t0ws11");
            Assert.AreEqual("cvcvcvbbwswswswswswswswswswsws", decompressed);
        }
    }
}
