using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unimob.Construction
{
    using NTPackage.Functions;

    public enum SelectorObjectType{
        Construction,
    }

    public class SelectorObject : NTBehaviour
    {
        public SelectorObjectType SelectorObjectType;

        public virtual void OnClick(){
            
        }
    }
}