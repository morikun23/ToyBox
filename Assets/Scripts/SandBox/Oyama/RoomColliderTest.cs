using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class RoomColliderTest : MonoBehaviour
    {
        public int m_roomNumber;

        PlayRoomManagerTest m_roomManager;

        void Start()
        {
            m_roomManager = FindObjectOfType<PlayRoomManagerTest>();
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