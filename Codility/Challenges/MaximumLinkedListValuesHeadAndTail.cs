using System;
using System.Collections.Generic;
using System.Text;

namespace Codility.Challenges
{
    public class MaximumLinkedListValuesHeadAndTail
    {
        public static int maximumPages(SinglyLinkedListNode head)
        {
            var dataList = new List<int>();
            var node = head;
            while(node != null)
            {
                dataList.Add(node.data);
                node = node.next;
            }

            var limit = dataList.Count / 2;
            int? highest = null;
            var lastIndex = dataList.Count - 1;
            for(int indx = 0; indx < limit; indx++)
            {
                var sum = dataList[indx] + dataList[lastIndex - indx];

                if (highest == null)
                {
                    highest = sum;
                }
                else if (sum > highest)
                {
                    highest = sum;
                }
            }

            return highest.Value;
        }


        public class SinglyLinkedListNode
        {
            public int data { get; set; }
            public SinglyLinkedListNode next { get; set; }
        }
    }
}
