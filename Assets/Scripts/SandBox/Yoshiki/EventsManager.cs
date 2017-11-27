using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class EventsManager : MonoBehaviour
    {
        [SerializeField]
        GameObject m_UI;

        int m_count = 0;

        BoxCollider2D[] m_buttons = new BoxCollider2D[5]; 

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {

            var childTransForm = m_UI.GetComponentsInChildren<Transform>();
            foreach(Transform child in childTransForm)
            {
                if (child.name.Contains("Button"))
                {
                    if (child.GetComponent<BoxCollider2D>() != null)
                    {
                        m_buttons[m_count] = child.GetComponent<BoxCollider2D>();
                        m_count++;
                        
                    }
                }
            }
            for (int i = 0; i < m_count; i++)
            {
                Debug.Log(m_buttons[i]);
            }
        }

        public void EventStart()
        {
            for (int i = 0; i < m_count; i++) 
            {
                m_buttons[m_count].enabled = false;
            }
        }
        public void EventEnd()
        {
            for (int i = 0; i < m_count; i++)
            {
                m_buttons[m_count].enabled = true;
            }
        }
    }
}