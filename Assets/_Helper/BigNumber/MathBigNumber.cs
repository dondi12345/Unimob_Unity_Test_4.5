using System;

namespace NTNumber
{
    public static class MathBigNumber
    {
        public static void Normalize(BigNumber number)
        {
            if (number == null)
                return;

            if (number.Base == 0)
            {
                number.Power = 0;
                return;
            }

            //2500 -> 2.5K
            while (Math.Abs(number.Base) >= 1000d)
            {
                number.Base /= 1000d;
                number.Power++;
            }

            // 0.002M -> 2K
            while (Math.Abs(number.Base) < 1d && number.Power > 0)
            {
                number.Base *= 1000d;
                number.Power--;
            }
        }

        public static BigNumber Add(BigNumber a, BigNumber b)
        {
            if (a == null)
                return Clone(b);

            if (b == null)
                return Clone(a);

            if (a.Base == 0)
                return Clone(b);

            if (b.Base == 0)
                return Clone(a);

            int powerDifference = a.Power - b.Power;

            // a greater than b
            if (powerDifference > 0)
            {
                // Too small
                if (powerDifference > 6)
                    return Clone(a);

                double convertedB =
                    b.Base / Math.Pow(1000d, powerDifference);

                return new BigNumber(
                    a.Base + convertedB,
                    a.Power
                );
            }

            // b greater than a
            if (powerDifference < 0)
            {
                int difference = -powerDifference;
                // Too small
                if (difference > 6)
                    return Clone(b);

                double convertedA =
                    a.Base / Math.Pow(1000d, difference);

                return new BigNumber(
                    convertedA + b.Base,
                    b.Power
                );
            }

            // Same Power
            return new BigNumber(
                a.Base + b.Base,
                a.Power
            );
        }

        public static BigNumber Subtract(BigNumber a, BigNumber b)
        {
            if (a == null)
                return new BigNumber();

            if (b == null || b.Base == 0)
                return Clone(a);

            if (IsGreaterThan(b, a))
                return new BigNumber();

            int powerDifference = a.Power - b.Power;

            // Too small
            if (powerDifference > 6)
                return Clone(a);

            // a greater than b
            if (powerDifference >= 0)
            {
                double convertedB =
                    b.Base / Math.Pow(1000d, powerDifference);

                return new BigNumber(
                    a.Base - convertedB,
                    a.Power
                );
            }

            // b greater than a
            double convertedA =
                a.Base / Math.Pow(1000d, -powerDifference);

            return new BigNumber(
                convertedA - b.Base,
                b.Power
            );
        }

        public static BigNumber Multiply(BigNumber a, BigNumber b)
        {
            if (a == null || b == null)
                return new BigNumber();

            return new BigNumber(
                a.Base * b.Base,
                a.Power + b.Power
            );
        }

        public static BigNumber Multiply(BigNumber a, double value)
        {
            if (a == null)
                return new BigNumber();

            return new BigNumber(
                a.Base * value,
                a.Power
            );
        }

        public static BigNumber Divide(BigNumber a, BigNumber b)
        {
            if (a == null)
                return new BigNumber();

            if (b == null || b.Base == 0)
                throw new DivideByZeroException();

            return new BigNumber(
                a.Base / b.Base,
                a.Power - b.Power
            );
        }

        public static BigNumber Divide(BigNumber a, double value)
        {
            if (a == null)
                return new BigNumber();

            if (value == 0)
                throw new DivideByZeroException();

            return new BigNumber(
                a.Base / value,
                a.Power
            );
        }

        public static bool IsGreaterThan(BigNumber a, BigNumber b)
        {
            if (a == null)
                return false;

            if (b == null)
                return true;

            if (a.Power > b.Power)
                return true;

            if (a.Power < b.Power)
                return false;

            return a.Base > b.Base;
        }

        public static bool IsGreaterThanOrEqual(BigNumber a, BigNumber b)
        {
            return IsGreaterThan(a, b) || IsEqual(a, b);
        }

        public static bool IsLessThan(BigNumber a, BigNumber b)
        {
            return !IsGreaterThanOrEqual(a, b);
        }

        public static bool IsEqual(BigNumber a, BigNumber b)
        {
            if (a == null || b == null)
                return a == b;

            if (a.Base == 0 && b.Base == 0)
                return true;

            if (a.Power != b.Power)
                return false;

            return Math.Abs(a.Base - b.Base) < 0.0000001d;
        }

        public static BigNumber Clone(BigNumber value)
        {
            if (value == null)
                return new BigNumber();

            return new BigNumber(
                value.Base,
                value.Power
            );
        }
    }
}