using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class PrototypeEvent : EventBase
    {
        int i = 0;

        /// <summary>
        /// イベント開始時
        /// </summary>
        public virtual void OnStartEvent()
        {
            Debug.Log("イベント開始時");
        }

        /// <summary>
        /// イベント処理中
        /// </summary>
        public virtual bool OnUpdateEvent()
        {
            Debug.Log("イベント処理中");
            i++;
            if(i == 100)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// イベント終了時
        /// </summary>
        public virtual void OnEndEvent()
        {
            Debug.Log("イベント終了時");
        }


    }
}