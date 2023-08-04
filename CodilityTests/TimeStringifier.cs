using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodilityTests
{
    [TestClass]
    public class TimeStringifier
    {

        public string solution(int X)
        {
            int min = 60;
            int hour = min * 60;
            int day = hour * 24;
            int week = day * 7;

            string time = "";
            int unitCount = 0;

            var weeks = DivRem(X, week, out int remainder);
            if (weeks > 0)
            {
                unitCount++;
                time += $"{weeks}w";
            }

            var days = DivRem(remainder, day, out remainder);
            if (days > 0)
            {
                if (++unitCount == 2)
                {
                    if (remainder > 0)
                        days++;

                    return time + $"{days}d";
                }
                else
                {
                    time += $"{days}d";
                }
            }

            var hours = DivRem(remainder, hour, out remainder);
            if(hours > 0)
            {
                if (++unitCount == 2)
                {
                    if (remainder > 0)
                        hours++;

                    return time + $"{hours}h";
                }
                else
                {
                    time += $"{hours}h";
                }
            }

            var mins = DivRem(remainder, min, out remainder);
            if (mins > 0)
            {
                if (++unitCount == 2)
                {
                    if (remainder > 0)
                        mins++;

                    return time + $"{mins}m";
                }
                else
                {
                    time += $"{mins}m";
                }
            }

            if (remainder == 0)
                return time;

            else
                return time += $"{remainder}s";
        }

        public int DivRem(int first, int second, out int remainder)
        {
            var quotient = first / second;
            remainder = quotient == 0 ? first : first % second;

            return quotient;
        }

        [TestMethod]
        public void Test()
        {
            var time = solution(100);
            Assert.AreEqual("1m40s", time);

            time = solution(7263);
            Assert.AreEqual("2h2m", time);

            time = solution(3605);
            Assert.AreEqual("1h5s", time);

            time = solution(60*60);
            Assert.AreEqual("1h", time);
        }
    }
}
