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

        Gate m_startGate;
        Gate m_endGate;
        [SerializeField]
        bool m_isStart;
        [SerializeField]
        bool m_isEnd;

        BoxCollider2D m_left;
        BoxCollider2D m_right;

        //移動値
        [SerializeField]
        float m_moveNum;

        Vector2 m_nowPos, m_targetPos;

        public bool m_isAction = false;

        private Rigidbody2D m_rigidbody;

        // Use this for initialization
        void Start()
        {
            m_nowPos = m_targetPos = Vector2.zero;
            
            //各種オブジェの参照
            m_startPoint = transform.Find("StartPoint").transform.position;
            m_endPoint = transform.Find("EndPoint").transform.position;

            m_startGate = transform.Find("StartPoint").transform.Find("StartGate").GetComponent<Gate>();
            m_endGate = transform.Find("EndPoint").transform.Find("EndGate").GetComponent<Gate>();

            m_elevator = transform.Find("Collider").gameObject;
            m_elevator.transform.position = m_startPoint;

            m_targetPos = m_endPoint;

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
            if (!m_isAction) return;

            m_nowPos = Vector2.MoveTowards(m_elevator.transform.position, m_targetPos, m_moveNum);

            if (m_nowPos == m_startPoint || m_nowPos == m_endPoint)
            {
                if (m_targetPos == m_startPoint)
                {
                    m_endGate.Close();
                    m_targetPos = m_endPoint;
                    m_isAction = false;
                }
                else
                {
                    m_startGate.Close();
                    m_targetPos = m_startPoint;
                    m_isAction = false;
                }

            }
            else
            {
                m_rigidbody.MovePosition(m_nowPos);
            }
        }

        //外部から作動させたい場合に
        public void Action()
        {
            m_isAction = true;
            m_isStart = !m_isStart;
            m_isEnd = !m_isEnd;

            if(m_isStart){ m_startGate.Open(); }
            if (m_isEnd) { m_endGate.Open(); }

        }

    }
}