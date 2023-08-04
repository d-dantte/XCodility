using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    class MinimumStringDeletions
    {
        public int solution(string S)
        {
            //build the occurence map
            var occurenceMap = new Dictionary<char, int>();
            foreach(var chr in S)
            {
                if (!occurenceMap.ContainsKey(chr))
                    occurenceMap[chr] = 0;

                else
                {
                    occurenceMap[chr]++;
                }
            }

            //sort the occurence map in descending order
            var comparer = Comparer<int>.Create((x, y) => y.CompareTo(x));
            var sortedMap = new SortedList<int, List<char>>(comparer);
            foreach(var chr in occurenceMap.Keys)
            {
                var occurence = occurenceMap[chr];
                if (!sortedMap.ContainsKey(occurence))
                    sortedMap.Add(occurence, new List<char>());

                sortedMap[occurence].Add(chr);
            }

            //find the deletions
            var deletionCount = 0;
            foreach(var key in sortedMap.Keys.ToList())
            {
                if(sortedMap[key].Count > 1)
                {

                }
            }

            throw new Exception("incomplete");
        }

        public int NextValidOccurenceCount(List<int> keys, int index)
        {
            for (int cnt = index + 1; cnt< keys.Count; cnt++)
            {

            }

            throw new Exception("incomplete");
        }
    }
}
