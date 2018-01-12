using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{

    public class PrototypeEvent : IEvent
    {

        int m_count = 0;

        /// <summary>
        /// イベント開始時
        /// </summary>
        public virtual void OnStart(GameObject arg_playerObject, GameObject arg_gimmick)
        {
            Debug.Log("イベント開始時");
        }

        /// <summary>
        /// イベント処理中
        /// </summary>
        public virtual bool OnUpdate()
        {
            m_count++;

            if(m_count >100)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// イベント終了時
        /// </summary>
        public virtual void OnEnd()
        {
            Debug.Log("イベント終了時");
        }


    }
}