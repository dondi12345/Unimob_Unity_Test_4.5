using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions
{
    public class ObjectPoolingConfig
    {
        public const string ProductSkin = "ProductSkin";
    }

    public class ObjectPoolingManager : NTBehaviour
    {
        public NTDictionary<string, List<Transform>> ObjectNTDictionary = new NTDictionary<string, List<Transform>>();
        public Transform Holder;

        public static ObjectPoolingManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ObjectPoolingManager.Instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            ObjectPoolingManager.Instance = this;
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadHolder();
        }

        protected void LoadHolder()
        {
            if (Holder != null) return;
            this.Holder = transform.Find("Holder");
        }

        public virtual void PushChildObjectIntoPooling(Transform trans)
        {
            List<Transform> listTrans = new List<Transform>();
            foreach (Transform item in trans)
            {
                listTrans.Add(item);
            }
            for (int i = listTrans.Count - 1; i >= 0; i--)
            {
                this.PushObjectIntoPooling(listTrans[i]);
            }
        }

        public void PushObjectIntoPooling(Transform trans)
        {
            trans.SetParent(this.Holder);
            trans.gameObject.SetActive(false);
            try
            {
                this.ObjectNTDictionary.Get(trans.name).Add(trans);
            }
            catch (System.Exception)
            {
                List<Transform> lsTrans = new List<Transform>();
                lsTrans.Add(trans);
                this.ObjectNTDictionary.Add(trans.name, lsTrans);
            }
        }

        public Transform PullObjectFromPooling(string nameOb)
        {
            try
            {
                Transform trans = this.ObjectNTDictionary.Get(nameOb)[0];
                this.ObjectNTDictionary.Get(nameOb).RemoveAt(0);
                trans.gameObject.SetActive(true);
                return trans;
            }
            catch (System.Exception)
            {
                try
                {
                    Debug.LogWarning
                    (this.ObjectNTDictionary.Get(nameOb).Count);
                }
                catch (System.Exception)
                { }
                return null;
            }
        }
        public T PullObjectFromPooling<T>(string nameOb)
        {
            Transform trans;
            try
            {
                trans = this.ObjectNTDictionary.Get(nameOb)[0];
                this.ObjectNTDictionary.Get(nameOb).RemoveAt(0);
                trans.gameObject.SetActive(true);
                return trans.GetComponent<T>();
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }
    }
}
