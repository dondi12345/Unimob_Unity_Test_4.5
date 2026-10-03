using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NTNumber;
using UnityEngine;

namespace Unimob.Player
{

    public enum PlayerCurrency{
        Gold,
        Diamond,
    }

    [System.Serializable]
    public class PlayerData
    {
        public BigNumber Gold;
        public BigNumber Diamond;

    }
}
