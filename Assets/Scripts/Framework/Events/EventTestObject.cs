using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class EventTestObject : MonoBehaviour
    {
        EventsManager m_eventManager;

        [SerializeField]
        bool m_isCheck = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(m_isCheck)
            {
                GameObject.FindObjectOfType<EventsManager>().GetEvent(new PrototypeEvent());
                m_isCheck = false;
            }
        }
    }
}