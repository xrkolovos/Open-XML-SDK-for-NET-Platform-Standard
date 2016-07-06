using System;
using System.Diagnostics;
using System.Globalization;

namespace Utility.Xml
{
    internal class XmlExceptionHelper
    {
        internal static string[] BuildCharExceptionArgs(string data, int invCharIndex)
        {
            return BuildCharExceptionArgs(data[invCharIndex], invCharIndex + 1 < data.Length ? data[invCharIndex + 1] : '\0');
        }

        internal static string[] BuildCharExceptionArgs(char[] data, int invCharIndex)
        {
            return BuildCharExceptionArgs(data, data.Length, invCharIndex);
        }

        internal static string[] BuildCharExceptionArgs(char[] data, int length, int invCharIndex)
        {
            Debug.Assert(invCharIndex < data.Length);
            Debug.Assert(invCharIndex < length);
            Debug.Assert(length <= data.Length);

            return BuildCharExceptionArgs(data[invCharIndex], invCharIndex + 1 < length ? data[invCharIndex + 1] : '\0');
        }

        internal static string[] BuildCharExceptionArgs(char invChar, char nextChar)
        {
            string[] aStringList = new string[2];

            // for surrogate characters include both high and low char in the message so that a full character is displayed
            if (XmlCharType.IsHighSurrogate(invChar) && nextChar != 0) {
                int combinedChar = XmlCharType.CombineSurrogateChar(nextChar, invChar);
                aStringList[0] = new string(new char[] { invChar, nextChar });
                aStringList[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", combinedChar);
            } else {
                // don't include 0 character in the string - in means eof-of-string in native code, where this may bubble up to
                if ((int)invChar == 0) {
                    aStringList[0] = ".";
                } else {
                    aStringList[0] = Convert.ToString(invChar, CultureInfo.InvariantCulture);
                }
                aStringList[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", (int)invChar);
            }
            return aStringList;
        }
    }

    internal abstract class BinHexEncoder
    {
        private const string s_hexDigits = "0123456789ABCDEF";
        private const int CharsChunkSize = 128;

        internal static string Encode(byte[] inArray, int offsetIn, int count)
        {
            if (null == inArray) {
                throw new ArgumentNullException(nameof(inArray));
            }
            if (0 > offsetIn) {
                throw new ArgumentOutOfRangeException(nameof(offsetIn));
            }
            if (0 > count) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (count > inArray.Length - offsetIn) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            char[] outArray = new char[2 * count];
            int lenOut = Encode(inArray, offsetIn, count, outArray);
            return new String(outArray, 0, lenOut);
        }

        private static int Encode(byte[] inArray, int offsetIn, int count, char[] outArray)
        {
            int curOffsetOut = 0, offsetOut = 0;
            byte b;
            int lengthOut = outArray.Length;

            for (int j = 0; j < count; j++) {
                b = inArray[offsetIn++];
                outArray[curOffsetOut++] = s_hexDigits[b >> 4];
                if (curOffsetOut == lengthOut) {
                    break;
                }
                outArray[curOffsetOut++] = s_hexDigits[b & 0xF];
                if (curOffsetOut == lengthOut) {
                    break;
                }
            }
            return curOffsetOut - offsetOut;
        } // function
    }
}