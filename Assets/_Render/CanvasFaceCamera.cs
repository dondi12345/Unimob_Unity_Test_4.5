using UnityEngine;

namespace Unimob
{
    public class CanvasFaceCamera : MonoBehaviour
    {
        public Transform Target;

        private void LateUpdate()
        {
            FaceCamera();
        }

        public void FaceCamera()
        {
            if (Target == null)
            {
                return;
            }

            Vector3 direction = Vector3.ProjectOnPlane(-Target.forward, Vector3.up);
            if (direction.sqrMagnitude < 0.0001f)
            {
                direction = Vector3.ProjectOnPlane(Target.up, Vector3.up);
            }

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
