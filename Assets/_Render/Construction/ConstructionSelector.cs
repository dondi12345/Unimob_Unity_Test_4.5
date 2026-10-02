using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using NTPackage.UI;
using Unimob.Product;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unimob.Construction
{

    public class ConstructionSelector : SelectorObject
    {
        public ConstructionRender ConstructionRender;

        public override void OnClick()
        {
            base.OnClick();
            this.ConstructionRender.OnPopup();
        }
    }
}