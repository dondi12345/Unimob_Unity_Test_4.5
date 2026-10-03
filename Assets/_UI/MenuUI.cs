using System.Collections;
using System.Collections.Generic;
using NTPackage.UI;
using UnityEngine;

namespace Unimob.UI
{
public class MenuUI : MonoBehaviour
{
    public void _OnclickUpgrade(){
        PopupManager.Instance.OnUI(PopupCode.UpgradeDataUI);
    }
}
}