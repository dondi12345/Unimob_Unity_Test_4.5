using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unimob.Delivery
{

    public class DeliveryRenderConfig{
        public const string IsMove = "IsMove";
        public const string IsCarryMove = "IsCarryMove";
        public const string IsEmpty = "IsEmpty";
    }


    public class HumanRenderSkin : MonoBehaviour
    {
        public Animator Anim;

        public void Idle(){
            this.Anim.SetBool(DeliveryRenderConfig.IsMove, false);
        }

        public void Move(){
            this.Anim.SetBool(DeliveryRenderConfig.IsMove, true);
            this.Anim.SetBool(DeliveryRenderConfig.IsEmpty, true);
        }

        public void CarryIdle(){
            this.Anim.SetBool(DeliveryRenderConfig.IsMove, false);
            this.Anim.SetBool(DeliveryRenderConfig.IsEmpty, false);
        }
    }
}
