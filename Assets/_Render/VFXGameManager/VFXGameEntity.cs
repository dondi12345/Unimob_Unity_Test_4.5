using System.Collections.Generic;
using DG.Tweening;
using NTPackage.Functions;
using UnityEngine;

namespace Rubik.VFX
{
    public class VFXGameEntity : MonoBehaviour
    {
        public virtual void Play(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;
        }

        public virtual void SetMoveTo(Transform target, float duration){
            transform.DOMove(target.position, duration).OnComplete(() => {
                ObjectPoolingManager.Instance.PushObjectIntoPooling(transform);
            });
        }

        public virtual void SetMoveBenzierTo(List<Vector3> targets, float duration){
            transform.DOPath(targets.ToArray(), duration, PathType.CubicBezier).OnComplete(() => {
                StartCoroutine(NTFunction.WaitSecond(0.2f, () => {
                    ObjectPoolingManager.Instance.PushObjectIntoPooling(transform);
                }));
            });
        }
    }
}