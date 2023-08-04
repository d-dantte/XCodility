using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class NumberRanking
    {
        public static int NumberOfRanks(int k, List<int> scores)
        {
            var orderedGroups = scores
                .GroupBy(score => score)
                .Select((grouping, index) => new KeyValuePair<int, int>(
                    grouping.Key,
                    grouping.Count()))
                .OrderByDescending(rank => rank.Key);

            var playersToLevelUp = 0;
            var rank = 0;
            KeyValuePair<int, int>? prevGroup = null;

            foreach(var group in orderedGroups)
            {
                if (prevGroup == null)
                    rank = 1;

                else if (prevGroup?.Value == 1)
                    rank++;

                else //prevGroup?.Value > 1
                    rank += prevGroup.Value.Value;

                if (rank > k)
                    break;

                playersToLevelUp += group.Value;
                prevGroup = group;
            }

            return playersToLevelUp;
        }
    }
}
