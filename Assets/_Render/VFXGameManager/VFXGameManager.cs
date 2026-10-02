using System.Collections.Generic;
using NTPackage;
using NTPackage.Functions;
using UnityEngine;

namespace Rubik.VFX
{
    public class VFXGameManager : NTBehaviour
    {
        public List<VFXGameEntity> ListVFXGameEntity;
        public NTDictionary<string, VFXGameEntity> DictionaryVFXGameEntity;
        public Transform Holder;
        public static VFXGameManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (VFXGameManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            VFXGameManager.Instance = this;
        }

        protected override void Start()
        {
            base.Start();
            foreach(VFXGameEntity vfx in this.ListVFXGameEntity){
                this.DictionaryVFXGameEntity.Add(vfx.name, vfx);
            }
        }

        #region Functions
        public VFXGameEntity InstantiateFX(string path, Vector3 position, Quaternion rotation)
        {
            VFXGameEntity fx = ObjectPoolingManager.Instance.PullObjectFromPooling<VFXGameEntity>(path);
            if (fx == null)
            {
                VFXGameEntity fxResource = this.DictionaryVFXGameEntity.Get(path);
                if (fxResource == null)
                {
                    NTLog.LogError("VFXGameManager:InstantiateFX FX is null, FxPath: " + path);
                    return null;
                }
                else
                {
                    fx = Instantiate(fxResource);
                }
            }
            fx.transform.name = path;
            fx.transform.SetParent(Holder);
            fx.Play(position, rotation);
            return fx;
        }
        #endregion
    }
}