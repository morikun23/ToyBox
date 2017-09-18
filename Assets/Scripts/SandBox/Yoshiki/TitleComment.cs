using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title
{
    public class TitleComment : MonoBehaviour {

        Vector3 STARTSIZE = new Vector3();
        SpriteRenderer m_renderer;
        Color m_color;

        public void Spawn()
        {
            m_renderer = GetComponent<SpriteRenderer>();
            
            m_color = m_renderer.color;
            m_color.a = 1;
            m_renderer.color = m_color;


        }
        public void Delete()
        {
            m_renderer = GetComponent<SpriteRenderer>();
            m_color = m_renderer.color;
            m_color.a = 0;
            m_renderer.color = m_color;

        }
    }
}