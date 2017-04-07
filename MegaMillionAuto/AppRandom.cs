using System;
using System.Security.Cryptography;

namespace MegaMillionAuto
{
    public class AppRandom
    {
        private RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public int Next(int max)
        {
            var byte8 = new byte[8];
            rng.GetBytes(byte8);
            var seed = BitConverter.ToUInt64(byte8, 0);
            var result = seed % (ulong)max;
            return (int)result;
        }
    }
}
