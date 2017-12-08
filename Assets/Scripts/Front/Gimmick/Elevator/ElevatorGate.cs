using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class ElevatorGate : MonoBehaviour
    {
        Animator m_animator;
        EdgeCollider2D m_collider;

        
        void Start()
        {
            m_animator = GetComponent<Animator>();
            m_collider = GetComponent<EdgeCollider2D>();
        }

        /// <summary>
        /// 開く場合
        /// </summary>
        public void Open()
        {
            m_animator.Play("Gate_Open");
            m_collider.enabled = false;
        }

        /// <summary>
        /// 閉じる場合
        /// </summary>
        public void Close()
        {
            m_animator.Play("Gate_Close");
            m_collider.enabled = true;
        }

    }
}