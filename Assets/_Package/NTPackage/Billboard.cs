using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;

namespace NTPackage.NTFunction
{
    public class Billboard : NTBehaviour
    {
        public bool IsScale = false;
        public const float BaseHeight = 192.8363f;
        protected override void Update()
        {
            base.Update();
            // transform.LookAt(Camera.main.transform);
            transform.LookAt(transform.position + Camera.main.transform.transform.rotation * Vector3.forward, Camera.main.transform.transform.rotation * Vector3.up);
            if (this.IsScale)
            {
                float scale = Camera.main.transform.localPosition.y / BaseHeight;
                transform.localScale = new Vector3(scale, scale, scale);
                // if (Camera.main.transform.TryGetComponent<GOOrbit>(out GOOrbit go))
                // {
                //     float scale_1 = go.distance / go.distanceMin;
                //     transform.localScale = new Vector3(scale_1, scale_1, scale_1);
                // }
            }
        }
    }

}
