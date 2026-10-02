using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using NTPackage.Functions;

namespace NTPackage.EventDispatcher
{
    public enum EventCode{
        ACC_LobbyUDP,
        ACC_LobbyOut,
        ACC_Update_PlayerData,
        ACC_Update_MyPlayerData,
        ACC_ReceiveEmotion,
        BattleDeck_TorchUpdate,
        BattleDeck_UpgradeCharacter,
        BattleDeck_CharacterTeamUpdate,
        BattleDeck_DoneLoad,
        BattleCard_ReciveChatCrossServer,
        BattleCard_ReciveChatServer,
        BattleCard_ReciveChatPrivate,
        BattleCard_UpdateMail,
        BattleCard_Update_Currency,
        BattleCard_CharacterTrainning_SelectCharacter,
        BattleCard_CharacterFilter,
        BattleCard_CharacterSort,
        ChangeAvatar,
        ChangeDisplayName,
        Chat_ReceiveChat,
        Chat_ReceiveHistoryChat,
        Chat_UpdateNotifyChat,
        NotificationController_UpdateNotification,
        Mail_UpdateNotifyMail,
        ChangeSkin,
        ChangeBaseCharacterCloth,
        SelectCharacter,
        LobbyGame_PlayerJoin,
        LobbyGame_PlayerLeave,        
        LobbyGame_PlayerUpdate,
        DodgeConquer_PlayerJoin,
        DodgeConquer_PlayerLeave,
        DodgeConquer_PlayerUpdate,
        DodgeConquer_PlayerUpdateInfo,
        DodgeConquer_Chat,
        DodgeConquer_GetWaveData,
        DodgeConquer_PlayerGetHeal,
        DodgeConquer_PlayerGetDamage,
        Marathon_PlayerJoin,
        Marathon_PlayerLeave,
        Marathon_PlayerUpdateInfo,
        Marathon_SpawnProps,
        JumpingGame_PlayerJoin,
        JumpingGame_PlayerLeave,
        JumpingGame_PlayerUpdateInfo,
        HeatPanGame_PlayerJoin,
        HeatPanGame_PlayerLeave,
        HeatPanGame_TrapAdd,
        HeatPanGame_TrapRemove,
        HeatPanGame_PlayerGetDamage,
        UpdateItemData,
        BoomGame_StuffAdd,
        BoomGame_StuffRemove,
        BoomGame_PlayerJoin,
        BoomGame_PlayerLeave,
        WaitingGame_PlayerJoin,
        WaitingGame_PlayerLeave,
    }

    public class EventListenerManager : MonoBehaviour
    {
        public NTDictionary<string, NTDictionary<string, Action<object>>> ActionsDictionary = new NTDictionary<string, NTDictionary<string, Action<object>>>();

        public static EventListenerManager instance;
        private void Awake()
        {
            if (EventListenerManager.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            EventListenerManager.instance = this;
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

