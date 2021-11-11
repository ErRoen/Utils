using System;

namespace Utils.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ToNearestQuarter(this decimal input)
        {
            return Math.Round(input * 4, MidpointRounding.ToEven) / 4;
        }
    }
}