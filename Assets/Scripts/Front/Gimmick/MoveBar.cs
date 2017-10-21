﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class MoveBar : MonoBehaviour
    {

        //端
        Vector2 m_StartPoint, m_EndPoint;

        //動くバー
        GameObject m_Bar;

        //移動値
        [SerializeField]
        float m_moveNum;
        
        Vector2 nowPos,targetPos;

        [SerializeField]
        bool m_actionFlag = true;

        // Use this for initialization
        void Start()
        {
            nowPos = targetPos = Vector2.zero;

            //各種オブジェの参照
            m_StartPoint = transform.FindChild("StartPoint").transform.position;
            m_EndPoint = transform.FindChild("EndPoint").transform.position;

            m_Bar = transform.FindChild("Bar").gameObject;
            m_Bar.transform.position = m_StartPoint;

            targetPos = m_EndPoint;
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_actionFlag) return;

            nowPos = Vector2.MoveTowards(m_Bar.transform.position, targetPos, m_moveNum);

            if (nowPos == m_StartPoint || nowPos == m_EndPoint)
            {
                if (targetPos == m_StartPoint)
                {
                    targetPos = m_EndPoint;
                }
                else
                {
                    targetPos = m_StartPoint;
                }

            }
            else
            {

				m_Bar.GetComponent<Rigidbody2D>().MovePosition(nowPos);
            }
        }


        //外部(スイッチとか？)から作動を開始させたい場合に呼ぶやつ
        void Action()
        {
            m_actionFlag = true;
        }

    }

}