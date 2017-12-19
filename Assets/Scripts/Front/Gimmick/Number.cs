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

        BoxCollider2D m_collider;

        public bool hited = false;

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_numOrder = transform.parent.transform.gameObject;
            m_order = m_numOrder.GetComponent<NumbersOrder>();
            m_animetor = GetComponent<Animator>();
            m_collider = GetComponent<BoxCollider2D>();
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
                    m_collider.enabled = false;
                    //AudioSource source = AppManager.Instance.m_audioManager.CreateSe("SE_NumberOrder_count");
                    //source.Play();

                    //新しい音でして
                    AudioManager.Instance.QuickPlaySE("SE_NumberOrder_count");

                }
                else
                {
                    m_order.Reset();

                }
            }

        }
    }
}