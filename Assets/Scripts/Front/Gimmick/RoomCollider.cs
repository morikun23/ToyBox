using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class RoomCollider : MonoBehaviour
    {

        [SerializeField]
        PlayRoom m_NextPlayRoom,m_PrevPlayRoom;

        void OnTriggerEnter2D(Collider2D arg_col)
        {
            if (arg_col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (m_PrevPlayRoom)
                {
                    m_PrevPlayRoom.SleepRoomGimmick();
                }
                if (m_NextPlayRoom)
                {
                    m_NextPlayRoom.gameObject.SetActive(true);
                    m_NextPlayRoom.SetCurrentRoom();
                }

            }
        }

    }

}