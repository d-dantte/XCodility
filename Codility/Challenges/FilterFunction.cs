using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    public record Person(long Id, string Name);

    public record CountryRanking(long PersonId, string Country, int Rank);

    public record RankedResult(long PersonId, int Rank);


    public class FilterFunction
    {
        public static IList<RankedResult> FilterByCountryWithRank(
            IList<Person> people,
            IList<CountryRanking> rankingData,
            IList<string> countryFilter,
            int minRank, int maxRank,
            int maxCount)
        {

            // create a map for countries to be used for ordering
            var countryOrder = countryFilter
                .Select((country, index) => (country, index))
                .ToDictionary(tuple => tuple.country.ToLower(), tuple => tuple.index);

            // create a set for countries to be used for filtering
            var countrySet = new HashSet<string>(countryFilter.Select(country => country.ToLower()));

            // create a map for people, used for accessing the names quickly
            var peopleMap = people.ToDictionary(person => person.Id, person => person);

            // create the filtered and ordered list
            var filteredResults = rankingData
                .Where(data => countrySet.Contains(data.Country.ToLower()))
                .Where(data => data.Rank >= minRank)
                .Where(data => data.Rank <= maxRank)
                .OrderBy(data => data.Rank)
                .ThenBy(data => countryOrder[data.Country.ToLower()])
                .ThenBy(data => peopleMap[data.PersonId].Name)
                .Select(data => new RankedResult(data.PersonId, data.Rank));

            // include all records, and don't break ranks
            int? currentRank = null;
            int count = maxCount;
            var finalResult = new List<RankedResult>();
            foreach (var result in filteredResults)
            {
                if(count > 0)
                {
                    finalResult.Add(result);
                    currentRank = result.Rank;
                    count--;
                }
                else if(result.Rank == currentRank)
                {
                    finalResult.Add(result);
                }
                else
                {
                    break;
                }
            }

            return finalResult;
        }
    }
}
