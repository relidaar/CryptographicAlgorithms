using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CryptographicAlgorithms.Helpers
{
    public static class MathHelper
    {
        private static readonly Random _rnd = new Random();

        public static int Mod(this int x, int m) => (x % m + m) % m;
        public static uint Mod(this uint x, int m) => (uint)(((int)x % m + m) % m);
        public static double Mod(this double x, int m) => (x % m + m) % m;
        public static BigInteger Mod(this BigInteger x, int m) => (x % m + m) % m;

        public static uint GetRandomPrime(uint max, uint min = 1)
        {
            int minInt = (int)min;
            int maxInt = (int)max;
            Func<uint> GetRandomUint = () => (uint)_rnd.Next(minInt, maxInt);

            uint value = GetRandomUint();
            while (!value.IsPrime())
            {
                value = GetRandomUint();
            }

            return value;
        }

        public static bool IsPrime(this uint value)
        {
            for (var i = 2; i <= value / 2; i++)
            {
                if (value % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static uint GetNod(uint a, uint b) =>
            a == 0 || b == 0 ? Math.Max(a, b)
            : a > b ? GetNod(a % b, b)
            : GetNod(a, b % a);

        public static int GetInverse(this int value, int n)
        {
            var inverse = 0;
            while (inverse * value % n != 1) inverse++;

            return inverse;
        }
    }
}
