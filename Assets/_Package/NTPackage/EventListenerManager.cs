using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using NTPackage.Functions;

namespace NTPackage.EventDispatcher
{
    public enum EventCode{
        ConstructionUpgrade,
    }

    public class EventListenerManager : MonoBehaviour
    {
        public NTDictionary<string, NTDictionary<string, Action<object>>> ActionsDictionary = new NTDictionary<string, NTDictionary<string, Action<object>>>();

        public static EventListenerManager Instance;
        private void Awake()
        {
            if (EventListenerManager.Instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            EventListenerManager.Instance = this;
        }

        public void PostEvent(EventCode eventCode,object data = null){
            NTLog.LogMessage("PostEvent "+ eventCode);
            NTDictionary<string,Action<object>> actions = this.ActionsDictionary.Get(eventCode.ToString());
            if(actions == null || actions.Dictionary.Count == 0) return;
            foreach (KeyValuePair<string, System.Action<object>> item in actions.Dictionary.ToList())
            {
                //try
                //{
                    item.Value.Invoke(data);
                //}
                //catch (System.Exception e)
                //{
                //    NTLog.LogError(e.Message);
                //    actions.Remove(item.Key);
                //}
            }
        }

        public void PostEventWithKey(EventCode eventCode, string key, object data = null){
            NTDictionary<string, Action<object>> actions = this.ActionsDictionary.Get(eventCode.ToString());
            if(actions == null) return;
            actions.Get(key)?.Invoke(data);
        }

        public void Register(EventCode eventCode, string key,Action<object> callback){
            NTLog.LogMessage(key+" Register "+ eventCode);
            NTDictionary<string, Action<object>> actions = this.ActionsDictionary.Get(eventCode.ToString());
            if(actions == null){
                actions = new NTDictionary<string, Action<object>>();
                this.ActionsDictionary.Add(eventCode.ToString(), actions);
            }
            actions.Add(key, callback);
        }

        public void RemoveListener(EventCode eventCode, string key)
        {
            NTDictionary<string, Action<object>> actions = this.ActionsDictionary.Get(eventCode.ToString());
            if (actions == null) return;
            actions.Remove(key);
        }
    }
}

