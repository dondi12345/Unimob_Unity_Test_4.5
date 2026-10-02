using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions{
    public class NTLog
    {
        public static void LogMessage(string message, GameObject gameObject = null)
        {
            // return;
            #if UNITY_EDITOR
            Debug.Log("<color=green>Message: </color>" + message, gameObject);
            #endif
        }
        public static void LogWarning(string message, GameObject gameObject = null)
        {
            return;
            #if UNITY_EDITOR
                Debug.LogWarning("<color=yellow>Warning: </color>" + message, gameObject);
            #endif
        }
        public static void LogError(string message, GameObject gameObject = null)
        {
            // #if UNITY_EDITOR
                Debug.LogError("<color=red>Error: </color>" + message, gameObject);
            // #endif
        }
    }
}
