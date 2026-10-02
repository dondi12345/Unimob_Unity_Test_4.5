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

        public void CarryMove(){
            this.Anim.SetBool(DeliveryRenderConfig.IsCarryMove, true);
            this.Anim.SetBool(DeliveryRenderConfig.IsEmpty, false);
        }

        public void CarryIdle(){
            this.Anim.SetBool(DeliveryRenderConfig.IsMove, false);
            this.Anim.SetBool(DeliveryRenderConfig.IsEmpty, false);
        }
    }
}
