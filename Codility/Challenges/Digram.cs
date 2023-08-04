using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class Digram
    {
        public static int Solution(string S)
        {
            //create a map to keep all the digrams in the string
            var map = new Dictionary<string, List<int>>();

            //loop through the string, grabbing all digrams, and adding their positions in a list mapped to the digram
            for (int cnt = 1; cnt < S.Length; cnt++)
            {
                var digram = S.Substring(cnt - 1, 2);

                if (!map.ContainsKey(digram))
                    map[digram] = new List<int>();

                map[digram].Add(cnt - 1);
            }

            var greatestDistance =  map
                //filter out digrams that occured only once
                .Where(kvp => kvp.Value.Count > 1)

                //get the distance between the farthest occuring identical digrams
                .Select(kvp => kvp.Value.Last() - kvp.Value.First())

                //get the greatest distance
                .OrderByDescending(value => value)

                //return the value
                .FirstOrDefault();

            return greatestDistance == 0 ? -1 : greatestDistance;
        }
    }
}
