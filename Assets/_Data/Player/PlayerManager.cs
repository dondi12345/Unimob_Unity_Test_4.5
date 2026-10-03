using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage.Functions;
using UnityEngine;

namespace Unimob.Player
{
    public class PlayerManager : NTBehaviour
    {
        [SerializeField]
        private PlayerData _playerData;

        public List<Sprite> ListIconCurrency;

        public static PlayerManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (PlayerManager.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
             }
            PlayerManager.Instance = this;
        }

        #region Load
        public void Init(){
            this._playerData = new PlayerData();
            this._playerData.Gold = new BigNumber(2,2);
            this._playerData.Diamond = new BigNumber(0);
        }
        #endregion 

        #region Function
        public void SpendGold(BigNumber amount){
            this._playerData.Gold = MathBigNumber.Subtract(this._playerData.Gold, amount);
        }
        public void AddGold(BigNumber amount){
            this._playerData.Gold = MathBigNumber.Add(this._playerData.Gold, amount);
        }
        #endregion

        #region Get
        public BigNumber GetGold(){
            return _playerData.Gold;
        }
        public BigNumber GetDiamond(){
            return _playerData.Diamond;
        }

        public Sprite GetIconCurrency(PlayerCurrency currency){
            return this.ListIconCurrency[(int)currency];
        }

        public BigNumber GetCurrency(PlayerCurrency currency){
            switch (currency){
                case PlayerCurrency.Gold:
                    return this.GetGold();
                case PlayerCurrency.Diamond:
                    return this.GetDiamond();
                default:
                    return new BigNumber(0);
            }
        }
        #endregion
    }
}
