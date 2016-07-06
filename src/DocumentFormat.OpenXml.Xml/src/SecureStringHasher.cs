using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Xml
{
    internal class SecureStringHasher : IEqualityComparer<String>
    {
        private int _hashCodeRandomizer;

        public SecureStringHasher()
        {
            _hashCodeRandomizer = Environment.TickCount;
        }


        public bool Equals(String x, String y)
        {
            return String.Equals(x, y, StringComparison.Ordinal);
        }

        public int GetHashCode(String key)
        {
            int hashCode = _hashCodeRandomizer;
            // use key.Length to eliminate the range check
            for (int i = 0; i < key.Length; i++) {
                hashCode += (hashCode << 7) ^ key[i];
            }
            // mix it a bit more
            hashCode -= hashCode >> 17;
            hashCode -= hashCode >> 11;
            hashCode -= hashCode >> 5;
            return hashCode;
        }
    }
}