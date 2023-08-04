using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class ShortestSubset
    {
        public static long Calculate(List<long> cars, int k)
        {
            if (k == 1 || k == 0)
                return k;

            var orderedCars = cars.OrderBy(car => car).ToArray();

            if (k > orderedCars.Length)
                return (orderedCars[orderedCars.Length - 1] - orderedCars[0]) + 1;

            int windowStart = 0, windowEnd = k - 1;

            long? roofLength = null;
            for(; windowEnd < cars.Count; windowStart++, windowEnd++)
            {
                var tempLength = CalculateRoofLength(cars[windowStart], cars[windowEnd]);
                if (roofLength == null || tempLength < roofLength)
                    roofLength = tempLength;
            }

            return roofLength ?? 0;
        }

        private static long CalculateRoofLength(long car1, long car2) => 1 + Math.Abs(car1 - car2);
    }
}
