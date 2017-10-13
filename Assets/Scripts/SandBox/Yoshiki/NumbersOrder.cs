using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class NumbersOrder : MonoBehaviour
    {

        [SerializeField]
        GameObject [] m_number;

        bool [] m_check;

        int m_nowCount;
        int m_nextCount;

        void Start()
        {
            Initialize();
            StartCoroutine("UpdateByFrame");
        }


        public void Initialize()
        {
            m_nowCount = 0;
            m_nextCount = m_nowCount++;

            for (int i = 0; i < m_number.Length; i++)
            {
                m_check[i] = m_number[i].GetComponent<Number>().hitNumber;

            }
        }

        public void UpdateByFrame()
        {
            if (m_check[0])
            {
                                
            }
         
        }
    }
}
