﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.DataStructures
{
    /// <summary>
    /// Provides fast, uniform-distribution hashing. Don't use for crypto.
    /// Used to support the bloom filters.  Not accesible outside this
    /// assembly.
    /// </summary>
    internal sealed class MurmurHash2
    {
        #region Private constants

        /// <summary>
        /// Spec defined constant
        /// </summary>
        private const UInt32 m = 0x5bd1e995;

        /// <summary>
        /// Spec defined constant
        /// </summary>
        private const Int32 r = 24;

        #endregion

        #region Public methods

        public UInt32 Hash(Byte[] data, UInt32 seed)
        {
            Int32 length = data.Length;
            if (length == 0)
                return 0;
            UInt32 h = seed ^ (UInt32)length;
            Int32 currentIndex = 0;
            while (length >= 4)
            {
                UInt32 k = (UInt32)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);
                k *= m;
                k ^= k >> r;
                k *= m;

                h *= m;
                h ^= k;
                length -= 4;
            }
            switch (length)
            {
                case 3:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (UInt32)(data[currentIndex] << 16);
                    h *= m;
                    break;
                case 2:
                    h ^= (UInt16)(data[currentIndex++] | data[currentIndex] << 8);
                    h *= m;
                    break;
                case 1:
                    h ^= data[currentIndex];
                    h *= m;
                    break;
                default:
                    break;
            }

            // Do a few final mixes of the hash to ensure the last few
            // bytes are well-incorporated.

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return h;
        }

        #endregion
    }
}
