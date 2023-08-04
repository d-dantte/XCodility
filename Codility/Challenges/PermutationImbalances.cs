using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class PermutationImbalances
    {
        public int FindAllImbalances(int[] list)
        {
            return this
                .AllGroups(list)
                .Select(RecordImbalance)
                .Sum();
        }

        public int[][] AllGroups(IEnumerable<int> list)
        {
            var returnList = new List<int[]>();
            for (int count = 2; count <= list.Count(); count++)
            {
                returnList.AddRange(
                    this.AdjacentGroups(list, count)
                        .Select(arr => arr
                            .OrderBy(t => t)
                            .ToArray())
                        .ToArray());
            }

            return returnList.ToArray();
        }

        public IEnumerable<int[]> AdjacentGroups(IEnumerable<int> list, int groupLength)
        {
            var count = list.Count() - groupLength;

            if (count < 0)
                yield return null;

            int index = 0;
            while(index <= count)
            {
                yield return list
                    .Skip(index)
                    .Take(groupLength)
                    .ToArray();

                index++;
            }
        }

        public int RecordImbalance(IEnumerable<int> list)
        {
            int? prev = null;
            foreach (var current in list)
            {
                if (prev != null && current - prev > 1)
                    return 1;

                prev = current;
            }

            return 0;
        }
    }
}
