using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility.Challenges
{
    public class RotationalCypher
    {

        public string Encrypt(string @string, int rotationDistance)
        {
            var rotator = new Rotator(rotationDistance);
            return @string
                .Select(rotator.RotateCharForward)
                .AsString();
        }
        public string Decrypt(string @string, int rotationDistance)
        {
            var rotator = new Rotator(rotationDistance);
            return @string
                .Select(rotator.RotateCharBackward)
                .AsString();
        }


        public class Rotator
        {
            private readonly int distance;

            public Rotator(int distance)
            {
                this.distance = Math.Abs(distance);
            }

            public char RotateCharForward(char @char)
            {
                if (char.IsDigit(@char))
                    return RotateDigitForward(@char);

                if (char.IsLetter(@char))
                    return RotateLetterForward(@char);

                return @char;
            }

            public char RotateCharBackward(char @char)
            {
                if (char.IsDigit(@char))
                    return RotateDigitBackward(@char);

                if (char.IsLetter(@char))
                    return RotateLetterBackward(@char);

                return @char;
            }

            private char RotateLetterForward(char originalChar)
            {
                if (!char.IsLetter(originalChar))
                    throw new Exception("Cannot rotate a non alphabet");

                var isCapital = char.IsUpper(originalChar);
                var numericValue = char.ToLower(originalChar) - 97;
                var rotatedValue = (numericValue + distance) % 26;
                var rotatedChar = (char)(rotatedValue + 97);

                return isCapital
                    ? char.ToUpper(rotatedChar)
                    : rotatedChar;
            }

            private char RotateLetterBackward(char originalChar)
            {
                if (!char.IsLetter(originalChar))
                    throw new Exception("Cannot rotate a non alphabet");

                var isCapital = char.IsUpper(originalChar);
                var numericValue = char.ToLower(originalChar) - 97;
                var rotatedValue = (numericValue - distance) % 26;

                if (rotatedValue < 0)
                    rotatedValue = 26 + rotatedValue;

                var rotatedChar = (char)(rotatedValue + 97);

                return isCapital
                    ? char.ToUpper(rotatedChar)
                    : rotatedChar;
            }

            private char RotateDigitForward(char originalChar)
            {
                if (!char.IsDigit(originalChar))
                    throw new Exception("Cannot rotate a non number");

                var numericValue = originalChar - 48;
                var rotatedValue = (numericValue + distance) % 10;
                return (char)(rotatedValue + 48);
            }

            private char RotateDigitBackward(char originalChar)
            {
                if (!char.IsDigit(originalChar))
                    throw new Exception("Cannot rotate a non number");

                var numericValue = originalChar - 48;
                var rotatedValue = (numericValue - distance) % 10;

                if (rotatedValue < 0)
                    rotatedValue = 10 + rotatedValue;

                return (char)(rotatedValue + 48);
            }
        }
    }

    public static class Extension
    {
        public static string AsString(this IEnumerable<char> chars) => new string(chars.ToArray());
    }
}
