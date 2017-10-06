using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class MoveBar : MonoBehaviour
    {

        //端
        Vector2 m_EndPoint1, m_EndPoint2;

        //動くバー
        GameObject m_Bar;

        //移動値
        [SerializeField]
        float m_moveNum;

        Vector2 unko,manko;

        // Use this for initialization
        void Start()
        {
            unko = manko = Vector2.zero;

            //各種オブジェの参照
            m_EndPoint1 = transform.FindChild("EndPoint1").transform.position;
            m_EndPoint2 = transform.FindChild("EndPoint2").transform.position;

            m_Bar = transform.FindChild("Bar").gameObject;
            m_Bar.transform.position = m_EndPoint1;

            manko = m_EndPoint2;
        }

        // Update is called once per frame
        void Update()
        {
            unko = Vector2.MoveTowards(m_Bar.transform.position, manko, m_moveNum);

            Debug.Log(unko);

            if (unko == m_EndPoint1 || unko == m_EndPoint2)
            {
                if (manko == m_EndPoint1)
                {
                    manko = m_EndPoint2;
                }
                else
                {
                    manko = m_EndPoint1;
                }

            }
            else
            {


                m_Bar.transform.position = unko;
            }
        }
    }

}