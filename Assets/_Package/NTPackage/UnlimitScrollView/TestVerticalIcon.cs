using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NTFunctions
{
    public class TestVerticalIcon : VerticalItem
    {
        public TextMeshProUGUI TextNumber;

        public override void UpdateData()
        {
            this.TextNumber.text = Index + "";
        }
    }
}