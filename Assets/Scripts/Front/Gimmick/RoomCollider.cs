using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class RoomCollider : MonoBehaviour
    {
        public int m_roomNumber;

        PlayRoomManager m_roomManager;

        void Start()
        {
            m_roomManager = FindObjectOfType<PlayRoomManager>();
        }

        void OnTriggerEnter2D(Collider2D arg_col)
        {
            if (arg_col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Enetr");
                m_roomManager.SetCurrentRoom(m_roomNumber);
            }
        }

    }

}