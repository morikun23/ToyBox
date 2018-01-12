using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class EventTestObject : MonoBehaviour
    {
        EventManager m_eventManager;
        GameObject m_player;

        [SerializeField]
        bool m_CanStart = false;

        // Update is called once per frame
        void Update()
        {
            if(m_CanStart)
            {
                m_player = GameObject.Find("Player");
                GameObject.FindObjectOfType<EventManager>().SetEvent(new PrototypeEvent(),gameObject,m_player);
                m_CanStart = false;
            }
        }
    }
}