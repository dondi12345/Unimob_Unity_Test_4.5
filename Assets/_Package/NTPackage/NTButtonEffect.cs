using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NTPackage.Functions;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace NTPackage.UI{
    public class NTButtonEffect : NTBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, ISubmitHandler
    {
        public bool AvailableClick = true;
        public Vector3 SizeUp = new Vector3(1.05f, 1.05f, 1.05f);
        public Vector3 SizeDown = new Vector3(0.95f, 0.95f, 0.95f);
        public Transform Main;

        public Transform Light;
        public Transform Dark;

        public UnityEvent Onclick;
        public UnityEvent Onhold;

        public Coroutine HoldCor;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if(!this.AvailableClick) return;
            if (this.HoldCor != null) StopCoroutine(this.HoldCor);
            this.HoldCor = StartCoroutine(this.Holding());
            if (this.Main == null) return;
            this.Main.DOScale(this.SizeDown, 0.1f);
        }

        IEnumerator Holding()
        {
            yield return new WaitForSeconds(0.5f);
            this.Onhold.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(!this.AvailableClick) return;
            if (this.HoldCor != null) StopCoroutine(this.HoldCor);
            if (this.Main == null) return;
            this.Main.DOScale(this.SizeUp, 0.1f).OnComplete(() =>
            {
                this.Main.DOScale(new Vector3(1f, 1f, 1f), 0.05f);
            });
        }

        public virtual void Chose()
        {
            if (this.Light != null && this.Dark != null)
            {
                this.Light.gameObject.SetActive(true);
                this.Dark.gameObject.SetActive(false);
            }
        }
        public virtual void UnChose()
        {
            if (this.Light != null && this.Dark != null)
            {
                this.Dark.gameObject.SetActive(true);
                this.Light.gameObject.SetActive(false);
            }
        }

        public bool IsChose()
        {
            if (this.Light == null) return false;
            return this.Light.gameObject.activeSelf;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!this.AvailableClick) return;
            this.Onclick.Invoke();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            // throw new NotImplementedException();
        }
    }
}
