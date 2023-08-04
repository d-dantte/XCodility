using System;

namespace Codility.Challenges
{
    class BinaryReduction
    {
        public int solution(string S)
        {
            if (!IsBinary(S))
                throw new Exception("Invalid string");


            var binary = S.TrimStart('0');
            var operationCount = 0;
            while (!"0".Equals(binary))
            {
                if (IsEven(binary))
                    binary = TrimLastChar(binary);

                else if (IsOdd(binary))
                    binary = $"{TrimLastChar(binary)}0";

                else
                {
                    throw new Exception("Invalid state");
                }

                operationCount++;
            }

            return operationCount;
        }
        private string TrimLastChar(string binary) => binary.Substring(0, binary.Length - 1);

        private bool IsBinary(string binary)
        {
            try
            {
                if (string.IsNullOrEmpty(binary))
                    return false;

                else
                {
                    _ = Convert.ToInt64(binary, 2);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool IsEven(string binary)
        {
            return
                binary != null
                && binary.Length > 0
                && binary[binary.Length - 1] == '0';
        }

        private bool IsOdd(string binary)
        {
            return
                binary != null
                && binary.Length > 0
                && binary[binary.Length - 1] == '1';
        }

    }

    class FastBinaryReduction2
    { 
        public int solution(string S)
        {
            var opCount = 0;
            var binary = S.TrimStart('0');
            for (int cnt = binary.Length - 1; cnt >= 0; cnt--)
            {
                opCount += binary[cnt] == 0 ? 1 : 2;
            }

            return opCount;
        }
    }
}
