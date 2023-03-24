using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace sas360_test
{
    public static  class Functions
    {
        public static IEnumerable<T> SliceRow<T>(this T[,] array, int row)
        {
            for (var i = array.GetLowerBound(1); i <= array.GetUpperBound(1); i++)
            {
                yield return array[row, i];
            }
        }

        public static IEnumerable<T> SliceColumn<T>(this T[,] array, int column)
        {
            for (var i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
            {
                yield return array[i, column];
            }
        }


        #region Set check bits

        public static int SetBitTo1(this int value, int position)
        {
            return value |= (1 << position);
        }

        public static int SetBitTo0(this int value, int position)
        {
            return value & ~(1 << position);
        }

        public static bool IsBitSetTo1(this int value, int position)
        {
            return (value & (1 << position)) != 0;
        }

        public static bool IsBitSetTo0(this int value, int position)
        {
            return !IsBitSetTo1(value, position);
        }

        #endregion

        public static float NextFloat()
        {
            Random random= new();
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }

    }
}
