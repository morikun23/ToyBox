using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class RoomColliderTest : MonoBehaviour
    {
        public int m_roomNumber;

        void OnTriggerEnter2D(Collider2D arg_col)
        {
            Debug.Log("Enetr");
            PlayRoomManagerTest.Instance.SetCurrentRoom(m_roomNumber);
        }

    }

}