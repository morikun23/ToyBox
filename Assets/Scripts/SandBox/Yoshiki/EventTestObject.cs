using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class EventTestObject : MonoBehaviour
    {
        EventManager m_eventManager;

        [SerializeField]
        bool m_CanStart = false;

        // Update is called once per frame
        void Update()
        {
            if(m_CanStart)
            {
                GameObject.FindObjectOfType<EventManager>().SetEvent(new PrototypeEvent(),gameObject);
                m_CanStart = false;
            }
        }
    }
}