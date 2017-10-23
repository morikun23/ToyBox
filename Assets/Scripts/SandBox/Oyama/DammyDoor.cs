using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{
    //PlayRoomの仕様上、どうしてもやっつけで出来てしまった糞スクリプトです。糞だけにどうか水に流してくれ…
    public class DammyDoor : MonoBehaviour
    {

        public GameObject m_ParentShutter;

        // Update is called once per frame
        void Update()
        {
            transform.position = m_ParentShutter.transform.position;
        }
    }

}