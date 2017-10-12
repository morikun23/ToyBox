using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class NumbersOrder : MonoBehaviour
    {

        [SerializeField]
        GameObject [] m_number;
        Number m_check;

        int m_nowCount = 0;
        int m_nextCount = 0;

        void Start()
        {
            Initialize();
        }
        void Update()
        {
            UpdateByFrame();
        }
        
        public void Initialize()
        {
            m_check = m_number[m_nowCount].GetComponent<Number>();    
        }

        public void UpdateByFrame()
        { 
            if (m_number[m_nowCount] == null)
            {
                
            }
            else
            {
                if (m_number[m_nowCount])
                {

                }
            }



        }
    }
}
