using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class PrefixSuffixMatch
    {
        public int solution(string S)
        {
            new int[] { }.Min();
            var cache = new Dictionary<string, int>();

            // extract prefixes and suffixes
            for(int count = 0; count <= S.Length; count++)
            {
                (var prefix, var suffix) = SplitString(S, count);

                // prefix
                if (cache.ContainsKey(prefix))
                    cache[prefix] = cache[prefix] + 1;

                else cache[prefix] = 1;

                // suffix
                if (cache.ContainsKey(suffix))
                    cache[suffix] = cache[suffix] + 1;

                else cache[suffix] = 1;
            }

            return cache
                .Where(kvp => kvp.Value > 1)
                .Where(kvp => kvp.Key.Length != S.Length) // we only want 'proper' pre-suf-fixes.
                .Select(kvp => kvp.Key)
                .Max(value => value.Length);
        }

        public (string prefix, string suffix) SplitString(string s, int count)
        {
            if (count == 0)
                return ("", s);

            else if (count == s.Length)
                return (s, "");

            return (s.Substring(0, count), s.Substring(count, s.Length - count));
        }
    }
}
