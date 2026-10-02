using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections.Generic;

namespace NTNumber
{
    public static class ServiceBigNumber
    {
        public static readonly List<string> ListUnit = new List<string>()
        {
            "",
            "K",
            "M",
            "B",
            "T",
            "Q",
        };

        public static string FormatBigNumber(BigNumber bigNumber)
        {
            if (bigNumber == null || bigNumber.Base == 0)
                return "0";

            if (bigNumber.Power < 0)
            {
                double value =
                    bigNumber.Base *
                    Math.Pow(1000d, bigNumber.Power);

                return value.ToString("0.##");
            }

            if (bigNumber.Power < ListUnit.Count)
            {
                return FormatBase(bigNumber.Base)
                       + ListUnit[bigNumber.Power];
            }

            return FormatBase(bigNumber.Base)
                   + "e"
                   + (bigNumber.Power * 3);
        }

        private static string FormatBase(double value)
        {
            double abs = Math.Abs(value);

            if (abs >= 100)
                return value.ToString("0");

            if (abs >= 10)
                return value.ToString("0.0");

            return value.ToString("0.00");
        }
    }
}

