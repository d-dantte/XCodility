using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility.Challenges
{
    public class AttendeeSelector
    {
        public List<CounrySchedule> CountrySchedule(List<Attendee> attendees)
        {
            return attendees
                .GroupBy(attendee => attendee.Country)
                .Select(FindSchedule)
                .ToList();
        }

        public CounrySchedule FindSchedule(IEnumerable<Attendee> attendees)
        {
            var dictionary = new Dictionary<DateTimeOffset, HashSet<Attendee>>();
            var attendeesArray = attendees.ToArray();
            foreach (var attendee in attendeesArray)
            {
                foreach (var availableDate in attendee.AvailableDates)
                {
                    dictionary
                         .GetOrAdd(availableDate, new HashSet<Attendee>())
                         .Add(attendee);
                }
            }

            var maxIndex = -1;
            var maxAttendanceCount = 0;
            var sortedDates = dictionary.Keys.OrderBy(x => x).ToArray();
            for (int index = 1; index < sortedDates.Length; index++)
            {
                if (AreConsecutiveDates(sortedDates[index - 1], sortedDates[index]))
                {
                    var completeAttendanceCount = CountCompleteAttendance(
                        dictionary[sortedDates[index - 1]],
                        dictionary[sortedDates[index]]);

                    if (completeAttendanceCount > maxAttendanceCount)
                    {
                        maxAttendanceCount = completeAttendanceCount;
                        maxIndex = index - 1;
                    }
                }
            }

            return new CounrySchedule
            {
                Name = attendeesArray[0].Country,
                StartDate = maxIndex > -1 ? sortedDates[maxIndex] : (DateTimeOffset?)null,
                Attendees = GetCompleteAttendance(
                    dictionary[sortedDates[maxIndex]],
                    dictionary[sortedDates[maxIndex + 1]])
            };
        }

        private List<string> GetCompleteAttendance(
            IEnumerable<Attendee> firstSet,
            IEnumerable<Attendee> secondSet)
        {
            var firstEmails = firstSet.Select(attendee => attendee.Email);
            var secondEmails = secondSet.Select(attendee => attendee.Email);

            return firstEmails
                .Intersect(secondEmails)
                .ToList();
        }

        private int CountCompleteAttendance(
            IEnumerable<Attendee> firstSet,
            IEnumerable<Attendee> secondSet)
            => GetCompleteAttendance(firstSet, secondSet).Count;

        private bool AreConsecutiveDates(DateTimeOffset date1, DateTimeOffset date2)
        {
            var span = date1 - date2;
            var absoluteDays = Math.Abs(span.TotalDays);

            if (absoluteDays < 2.0d)
            {
                //next day
                if (date1.Day != date2.Day)
                    return true;

                // same day
                else return false;
            }
            else return false;
        }
    }

    public class Attendee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public List<DateTimeOffset> AvailableDates { get; private set; } = new List<DateTimeOffset>();
    }

    public class CounrySchedule
    {
        public int AttendeeCount => Attendees?.Count ?? 0;
        public List<string> Attendees { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? StartDate { get; set; }
    }

    public static class Extensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this
            IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            if (!dictionary.ContainsKey(key))
                return dictionary[key] = value;

            else return dictionary[key];
        }

        public static void main()
        {
            var xyz = @"

";
        }
    }

}
