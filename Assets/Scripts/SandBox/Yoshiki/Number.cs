using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Number : MonoBehaviour
    {
        GameObject m_numOrder;

        [SerializeField]
        int m_hitNumber;

        NumbersOrder m_order;

        Animator m_animetor;

        public bool hited = false;

        void Start()
        {
            Initialize();
            
        }

        public void Initialize()
        {
            m_numOrder = transform.root.gameObject;
            m_order = m_numOrder.GetComponent<NumbersOrder>();
            m_animetor = GetComponent<Animator>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            bool m_hitflg = GetComponent<BoxCollider2D>().IsTouchingLayers(1 << LayerMask.NameToLayer("Player"));

            if (m_hitflg && !hited)
            {
                if(m_hitNumber == m_order.count)
                {
                    m_order.Match();
                    m_animetor.SetBool("light",true);
                    hited = true;
                    GetComponent<BoxCollider2D>().enabled = false;

                }
                else
                {
                    m_order.Reset();

                }
            }

        }
    }
}