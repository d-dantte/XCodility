using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class FamilySeatReservationOnPlane
    {
        public int Solution(int N, string S)
        {
            var rowReservations = (S ?? "")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(
                    x => int.Parse(x.Substring(0, x.Length - 1)),
                    x => x.Substring(x.Length - 1))
                .ToDictionary(
                    group => group.Key,
                    group => new HashSet<string>(group));

            return Enumerable
                .Range(1, N)
                .Select(x => PossibleFamilyReservationCount(x, rowReservations))
                .Sum();
        }

        private int PossibleFamilyReservationCount(int row, Dictionary<int, HashSet<string>> rowReservations)
        {
            var reservations = rowReservations.ContainsKey(row)
                ? rowReservations[row]
                : null;

            if (reservations == null)
                return 2;

            if (IsRegionFree(1, reservations) && IsRegionFree(3, reservations))
                return 2;

            if (IsRegionFree(1, reservations)
                || IsRegionFree(2, reservations)
                || IsRegionFree(3, reservations))
                return 1;

            return 0;
        }

        private bool IsRegionFree(int region, HashSet<string> rowReservations)
        {
            if (rowReservations == null)
                return true;

            if (region == 1)
            {
                return !rowReservations.Contains("B")
                    && !rowReservations.Contains("C")
                    && !rowReservations.Contains("D")
                    && !rowReservations.Contains("E");
            }
            if (region == 2)
            {
                return !rowReservations.Contains("D")
                    && !rowReservations.Contains("E")
                    && !rowReservations.Contains("F")
                    && !rowReservations.Contains("G");
            }
            if (region == 3)
            {
                return !rowReservations.Contains("F")
                    && !rowReservations.Contains("G")
                    && !rowReservations.Contains("H")
                    && !rowReservations.Contains("J");
            }

            return false;
        }

        [TestMethod]
        public void Tests()
        {
            var n = 2;
            var S = "1A 2F 1C";
            var result = Solution(n, S);
            Assert.AreEqual(2, result);

            n = 1;
            S = "";
            result = Solution(n, S);
            Assert.AreEqual(2, result);

            n = 22;
            S = "1A 3C 2B 20G 5A";
            result = Solution(n, S);
            Assert.AreEqual(41, result);
        }
    }
}
