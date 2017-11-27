using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class Elevator : MonoBehaviour
    {

        //端
        Vector2 m_startPoint, m_endPoint;

        //エレベーター
        GameObject m_elevator;

        BoxCollider2D m_left;
        BoxCollider2D m_right;

        //移動値
        [SerializeField]
        float m_moveNum;

        Vector2 nowPos, targetPos;

        public bool actionFlag = false;

        private Rigidbody2D m_rigidbody;

        // Use this for initialization
        void Start()
        {
            nowPos = targetPos = Vector2.zero;

            //各種オブジェの参照
            m_startPoint = transform.Find("StartPoint").transform.position;
            m_endPoint = transform.Find("EndPoint").transform.position;

            m_elevator = transform.Find("Collider").gameObject;
            m_elevator.transform.position = m_startPoint;

            targetPos = m_endPoint;

            //TODO:プレハブへRigidbodyを追加させる
            if (m_rigidbody == null)
            {
                m_rigidbody = m_elevator.gameObject.AddComponent<Rigidbody2D>();
                m_rigidbody.isKinematic = true;
                m_rigidbody.freezeRotation = true;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (!actionFlag) return;

            nowPos = Vector2.MoveTowards(m_elevator.transform.position, targetPos, m_moveNum);

            if (nowPos == m_startPoint || nowPos == m_endPoint)
            {
                if (targetPos == m_startPoint)
                {
                    
                    targetPos = m_endPoint;
                    actionFlag = false;
                }
                else
                {
                    targetPos = m_startPoint;
                    actionFlag = false;
                }

            }
            else
            {

                m_rigidbody.MovePosition(nowPos);
            }
        }

        //外部から作動させたい場合に
        public void Action()
        {
            actionFlag = true;
        }

    }
}