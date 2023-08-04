using System;
using System.Linq;

namespace Codility.Challenges
{
    public class BubbleSort
    {
        public T[] Sort<T>(T[] array, out int swapCount)
        where T : IComparable<T>
        {
            swapCount = 0;

            if (!NeedsSorting(array))
                return array;

            // find the min and max indices
            (int index, T value) min = (-1, default);
            (int index, T value) max = (-1, default);
            for (int index = 0; index < array.Length; index++)
            {
                if (min.index == -1)
                {
                    min = (index, array[index]);
                }
                else if (min.value.CompareTo(array[index]) > 0)
                {
                    min = (index, array[index]);
                }

                if (max.index == -1)
                {
                    max = (index, array[index]);
                }
                else if (max.value.CompareTo(array[index]) < 0)
                {
                    max = (index, array[index]);
                }
            }

            // bubble the min value
            if (min.index > 0)
            {
                swapCount += BubbleUp(array, min.index);
            }

            // bubble the max value
            if (max.index < array.Length - 1)
            {
                if (array[max.index].CompareTo(max.value) != 0)
                    max.index++;

                swapCount += BubbleDown(array, max.index);
            }

            return array;
        }

        public bool NeedsSorting<T>(T[] array)
        where T : IComparable<T>
        {
            var first = array[0];
            var last = array[array.Length - 1];

            if (first.CompareTo(last) > 0)
                return true;

            return array.Any(t => t.CompareTo(first) < 0 || t.CompareTo(last) > 0); 
        }

        public int BubbleUp<T>(T[] array, int index)
        where T : IComparable<T>
        {
            int swaps = 0;
            do
            {
                (array[index - 1], array[index]) = (array[index], array[index - 1]);
                index--;
                swaps++;
            }
            while (index >= 1);

            return swaps;
        }

        public int BubbleDown<T>(T[] array, int index)
        where T : IComparable<T>
        {
            int swaps = 0;
            do
            {
                (array[index + 1], array[index]) = (array[index], array[index + 1]);
                index++;
                swaps++;
            }
            while (index < array.Length - 1);

            return swaps;
        }
    }
}
