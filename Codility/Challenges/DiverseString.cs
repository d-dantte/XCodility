using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    public class DiverseString
    {
        public static string solution(int A, int B, int C)
        {
            //get a map of 
            var letterCount = new Dictionary<char, int>
            {
                ['a'] = A,
                ['b'] = B,
                ['c'] = C
            };

            //sort the characters in descending order or char count, taking only characters with counts higher than 0
            var orderedChars = letterCount
                .OrderByDescending(kvp => kvp.Value)
                .Where(kvp => kvp.Value > 0)
                .Select(kvp => kvp.Key)
                .ToList();

            //if all our char counts were 0 or less...
            if (orderedChars.Count <= 0)
                return "";

            var stringBuilder = new StringBuilder();
            int cycleIndex = 0;
            int repetition = 0;
            char? prevChar = null;
            while(orderedChars.Count > 0)
            {
                var chr = orderedChars[cycleIndex];
                stringBuilder.Append(chr);

                //if we are repeating, indicate
                if (prevChar == chr)
                {
                    repetition++;
                }
                else
                {
                    prevChar = chr;
                    repetition = 1;
                }

                //decrement the character count
                letterCount[chr] = letterCount[chr] - 1;

                //no more characters to use, so remove from our cycle, or we have repeated the same character twice already...
                if (letterCount[chr] == 0 || repetition == 2)
                {
                    orderedChars.Remove(chr);
                    cycleIndex--;
                }

                //cycle through our ordered characters
                if(orderedChars.Count > 0)
                    cycleIndex = (++cycleIndex) % orderedChars.Count;
            }

            return stringBuilder.ToString();
        }
    }
}
