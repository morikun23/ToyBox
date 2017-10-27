using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title
{
    public class TitleComment : MonoBehaviour {

        [SerializeField]
        GameObject m_this;

        SpriteRenderer m_renderer;

        public void Initialize()
        {
            m_renderer = m_this.GetComponent<SpriteRenderer>();
        }

        public void Spawn()
        {
            m_renderer.color = new Color(1, 1, 1, 1);
        }
        public void Delete()
        {
            m_renderer.color = new Color(1, 1, 1, 0);
        }
    }
}