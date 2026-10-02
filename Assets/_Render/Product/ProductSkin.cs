using System.Collections;
using UnityEngine;

namespace Unimob.Product
{
    public class ProductSkin : MonoBehaviour
    {
        public float FlyDuration = 0.3f;
        public float FlyHeight = 1f;

        private Coroutine _flyRoutine;

        public void FlyTo(Transform parent)
        {
            this.transform.SetParent(parent, true);
            if (this._flyRoutine != null)
            {
                this.StopCoroutine(this._flyRoutine);
            }
            this._flyRoutine = this.StartCoroutine(this.FlyRoutine());
        }

        private IEnumerator FlyRoutine()
        {
            Vector3 startPos = this.transform.localPosition;
            Quaternion startRot = this.transform.localRotation;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / this.FlyDuration;
                float k = Mathf.Clamp01(t);
                Vector3 pos = Vector3.Lerp(startPos, Vector3.zero, k);
                pos.y += Mathf.Sin(k * Mathf.PI) * this.FlyHeight;
                this.transform.localPosition = pos;
                this.transform.localRotation = Quaternion.Slerp(startRot, Quaternion.identity, k);
                yield return null;
            }
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
            this._flyRoutine = null;
        }
    }
}
