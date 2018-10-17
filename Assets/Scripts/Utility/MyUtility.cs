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

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ")";
        }
    }

    public static class Util
    {
        public static int Range(int now, int min, int max)
        {
            if (now > max) { return max; }
            if (now < min) { return min; }
            return now;
        }

        public static int Range(int now, int min, int max, int value)
        {
            if (now + value > max) { return max; }
            if (now + value < min) { return min; }
            return now + value;
        }

        public static int Roop(int now, int min, int max)
        {
            if (now > max) { return min; }
            if (now < min) { return max; }
            return now;
        }

        public static int Roop(int now, int min, int max, int value)
        {
            if (now + value > max) { return min; }
            if (now + value < min) { return max; }
            return now + value;
        }
    }
}
