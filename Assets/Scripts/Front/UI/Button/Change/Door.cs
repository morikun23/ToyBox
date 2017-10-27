using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Door : MonoBehaviour
    { 
        [SerializeField]
        GameObject m_chose;

        int m_count = 0;

        float alpha = 0.015f;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().Play("Door");
                m_chose.GetComponent<Choses>().Create();
                
            }
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            bool m_hitflg = GetComponent<BoxCollider2D>().IsTouchingLayers(1 << LayerMask.NameToLayer("Player"));

            if (m_hitflg && m_count == 0)
            {
                GetComponent<Animator>().Play("Door");
                m_chose.GetComponent<Choses>().Create();
                m_count++;
            }
        }

    }
}