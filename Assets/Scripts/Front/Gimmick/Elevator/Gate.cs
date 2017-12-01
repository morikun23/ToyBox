using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox.Yoshiki
{
    public class Gate : MonoBehaviour
    {
        Animator m_animator;
        EdgeCollider2D m_collider;

        // Use this for initialization
        void Start()
        {
            m_animator = GetComponent<Animator>();
            m_collider = GetComponent<EdgeCollider2D>();
        }

        public void Open()
        {
            m_animator.Play("Gate_Open");
            m_collider.enabled = false;
        }

        public void Close()
        {
            m_animator.Play("Gate_Close");
            m_collider.enabled = true;
        }

    }
}