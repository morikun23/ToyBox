using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class NumbersOrder : MonoBehaviour
    {
        [SerializeField]
        GameObject [] m_objectCount;

        Number [] m_objectNumber = new Number[5];
        Animator [] m_objectAnimator = new Animator[5];
        BoxCollider2D [] m_objectCollider = new BoxCollider2D[5];

        //このボタンで開くシャッター
        [SerializeField]
        Shutter m_scr_shutter;

        public int count = 0;

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {

            for (int i = 0; i < m_objectCount.Length; i++)
            {
                m_objectNumber[i] = m_objectCount[i].GetComponent<Number>();
                m_objectAnimator[i] = m_objectCount[i].GetComponent<Animator>();
                m_objectCollider[i] = m_objectCount[i].GetComponent<BoxCollider2D>();
            }
        }

        public void Match()
        {
            Debug.Log("hit");
            Debug.Log(count);
            count++;
            if (count == m_objectCount.Length) 
            {
                Debug.Log("クリア");
                //クリアした際の処理
                StartCoroutine(m_scr_shutter.OpenMoveCamera());
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
                    m_objectNumber[i].hited = false;
                    m_objectAnimator[i].SetBool("light", false);
                    m_objectCollider[i].enabled = true;

                }
            }
        }

    }
}
