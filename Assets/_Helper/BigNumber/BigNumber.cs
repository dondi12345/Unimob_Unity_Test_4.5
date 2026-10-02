namespace NTNumber
{
    [System.Serializable]
    public class BigNumber
    {
        public double Base;
        public int Power;

        public BigNumber()
        {
            Base = 0;
            Power = 0;
        }

        public BigNumber(double baseValue, int power = 0)
        {
            Base = baseValue;
            Power = power;

            MathBigNumber.Normalize(this);
        }
    }
}

