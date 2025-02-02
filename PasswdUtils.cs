using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Room_light
{
    internal class PasswdUtils
    {

        public static string Encrypt(string publicKey, int exponent, string password)
        {
            string md5_passwd = ComputeMD5Hash(password);

            BigInteger modulus = BigInteger.Parse(publicKey);
            BigInteger bytes = BytesToLong(Encoding.ASCII.GetBytes(md5_passwd));
            BigInteger powered = BigInteger.ModPow(bytes, exponent, modulus);

            StringBuilder result = new StringBuilder();
            result.Append("0000000000000000000000000000000000000000000000000000000000000000");
            result.Append(powered.ToString("X"));
            
            return result.ToString().ToLower();
        }

        public static BigInteger BytesToLong(byte[] s)
        {
            BigInteger acc = 0;

            int length = s.Length;
            int remainder = length % 4;

            if (remainder != 0)
            {
                int extra = 4 - remainder;
                byte[] padded = new byte[length + extra];
                Array.Copy(s, 0, padded, extra, length);
                s = padded;
                length += extra;
            }

            for (int i = 0; i < length; i += 4)
            {
                byte[] chunk = new byte[4];
                Array.Copy(s, i, chunk, 0, 4);

                if (BitConverter.IsLittleEndian) Array.Reverse(chunk);
                uint value = BitConverter.ToUInt32(chunk, 0);

                acc = (acc << 32) + value;
            }

            return acc;
        }

        private static string ComputeMD5Hash(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(passwordBytes);

                StringBuilder hexString = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    hexString.AppendFormat("{0:x2}", b);
                }

                return hexString.ToString();
            }
        }

        public static string MobileHash(string data)
        {
            using (var md5 = MD5.Create())
            {
                var prehash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
                var prehashHex = BitConverter.ToString(prehash).Replace("-", "").ToLower();

                return prehashHex.Substring(8, 8) +
                       prehashHex.Substring(0, 8) +
                       prehashHex.Substring(24, 8) +
                       prehashHex.Substring(16, 8);
            }
        }
    }
}
