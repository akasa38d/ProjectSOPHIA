using System;

namespace MyUtility
{
    static class Extensions
    {
        public static string ToStringMoney(this int money)
        {
            return String.Format("{0:#,0}", money);
        }
    }

    public class IntVector2
    {
        public int X;
        public int Y;

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static class Util
    {
        public static void Range(ref int now, int min, int max)
        {
            if (now > max) { now = max; }
            if (now < min) { now = min; }
        }

        public static void Range(ref int now, int min, int max, int value)
        {
            if (now + value > max) { now = max; }
            else if (now + value < min) { now = min; }
            else { now += value; }
        }

        public static void Roop(ref int now, int min, int max)
        {
            if (now > max) { now = min; }
            if (now < min) { now = max; }
        }
    }
}
