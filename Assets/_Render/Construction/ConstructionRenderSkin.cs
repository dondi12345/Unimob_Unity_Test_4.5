using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unimob.Construction
{
    public class ConstructionRenderSkinConfig{
        public const string AnimBoxIdle = "BoxIdle";
        public const string AnimBoxOpen = "BoxOpen";
    }

    public class ConstructionRenderSkin : MonoBehaviour
    {
        public Transform TransProduct;
        public Transform TransBox;

        public Animation AnimatorBox;

        public void Init(){
            this.TransProduct.gameObject.SetActive(false);
            this.TransBox.gameObject.SetActive(true);
            this.AnimatorBox.Play(ConstructionRenderSkinConfig.AnimBoxIdle);
        }

        public void OpenBox(){
            this.AnimatorBox.Play(ConstructionRenderSkinConfig.AnimBoxOpen);
        }

        public void Unlock(){
            this.TransProduct.gameObject.SetActive(true);
            this.TransBox.gameObject.SetActive(false);
        }
    }
}
