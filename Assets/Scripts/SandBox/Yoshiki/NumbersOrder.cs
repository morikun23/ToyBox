using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class NumbersOrder : MonoBehaviour
    {
        [SerializeField]
        GameObject [] m_objectCount;

        public int count = 0;

        public void Match()
        {
            Debug.Log("hit");
            Debug.Log(count);
            count++;
            if (count == m_objectCount.Length) 
            {
                Debug.Log("クリア");
                //クリアした際の処理
            }
        }

        public void Reset()
        {
            if (count != 0)
            {
                Debug.Log("out");
                count = 0;
                for (int i = 0; i < m_objectCount.Length; i++)
                {
                    m_objectCount[i].GetComponent<Number>().hited = false;
                    m_objectCount[i].GetComponent<Animator>().SetBool("light", false);
                    m_objectCount[i].GetComponent<BoxCollider2D>().enabled = true;

                }
            }
        }

    }
}
