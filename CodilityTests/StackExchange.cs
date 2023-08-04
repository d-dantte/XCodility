using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    [TestClass]
    public class StackExchange
    {
        [TestMethod]
        public void Method()
        {
            int[] test = new[] { 5 };
            var result = solution(test);
            Assert.AreEqual(2, result);

            test = new[] { 2, 3 };
            result = solution(test);
            Assert.AreEqual(1, result);

            test = new[] { 4, 0, 3, 0 };
            result = solution(test);
            Assert.AreEqual(1, result);

            test = new[] { 1, 0, 4, 1 };
            result = solution(test);
            Assert.AreEqual(3, result);
        }


        public int solution(int[] A)
        {
            return Stacks(A).Sum();
        }

        public IEnumerable<int> Stacks(int[] A)
        {
            int carry = 0;
            int exchange;
            for (int index = 0; index < A.Length; index++)
            {
                yield return maximumExchange(A[index] + carry, out exchange);
                carry = exchange;
            }

            while(carry > 0)
            {
                yield return maximumExchange(carry, out exchange);
                carry = exchange;
            }
        }

        private int maximumExchange(int stackSize, out int carry)
        {
            carry = Math.DivRem(stackSize, 2, out var result);
            return result;
        }
    }
}
