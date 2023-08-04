using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    public class StringComparer
    {
        public bool Solution(string S, string T)
        {
            var expandedS = ExpandString(S);
            var expandedT = ExpandString(T);

            return AreEquivalent(expandedS, expandedT);
        }

        private bool AreEquivalent(string first, string second)
        {
            if (first.Length != second.Length)
                return false;

            for(int index = 0; index < first.Length; index++)
            {
                if (first[index] != '?' && second[index] != '?' && first[index] != second[index])
                    return false;
            }

            return true;
        }

        private string ExpandString(string stringValue)
        {
            var sb = new StringBuilder();
            var digits = "";
            foreach(var c in stringValue)
            {
                if(char.IsDigit(c))
                {
                    digits += c;
                }
                else
                {
                    if(digits.Length > 0)
                    {
                        sb.Append(QuestionMarks(int.Parse(digits)));
                        digits = "";
                    }

                    sb.Append(c);
                }
            }

            if (digits.Length > 0)
            {
                sb.Append(QuestionMarks(int.Parse(digits)));
            }

            return sb.ToString();
        }

        private string QuestionMarks(int repetitions)
        {
            return new StringBuilder()
                .Append(Enumerable
                    .Repeat('?', repetitions)
                    .ToArray())
                .ToString();
        }
    }
}
