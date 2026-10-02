using NTPackage.Functions;
using UnityEngine;

namespace NTPackage
{
    public class DontDestroyOnLoad : NTBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
