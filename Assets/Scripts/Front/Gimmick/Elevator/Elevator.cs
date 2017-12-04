using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Elevator : MonoBehaviour
    {

        //端
        private Vector2 m_startPoint, m_endPoint;

        //エレベーター
        private GameObject m_collider;

        private Gate m_startGate;
        private Gate m_endGate;
        [SerializeField]
        bool m_isStart;
        [SerializeField]
        bool m_isEnd;

        //移動値
        [SerializeField]
        float m_moveNum;

        private Vector2 m_nowPos, m_targetPos;

        public bool m_isAction = false;

        private Rigidbody2D m_rigidbody;

        void Start()
        {
            m_nowPos = m_targetPos = Vector2.zero;

            //#region ~ #endregion各種オブジェの参照
            m_startPoint = transform.Find("StartPoint").transform.position;
            m_endPoint = transform.Find("EndPoint").transform.position;

            m_startGate = transform.Find("StartPoint").transform.Find("StartGate").GetComponent<Gate>();
            m_endGate = transform.Find("EndPoint").transform.Find("EndGate").GetComponent<Gate>();

            m_collider = transform.Find("Collider").gameObject;
            m_collider.transform.position = m_startPoint;

            m_targetPos = m_endPoint;

            if (m_rigidbody == null)
            {
                m_rigidbody = m_collider.gameObject.AddComponent<Rigidbody2D>();
                m_rigidbody.isKinematic = true;
                m_rigidbody.freezeRotation = true;
            }

        }


        void Update()
        {
            if (!m_isAction) return;

            m_nowPos = Vector2.MoveTowards(m_collider.transform.position, m_targetPos, m_moveNum);

            if (IsMoveComleted())
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

        ///<summary>外部から作動させたい場合に</summary> 
        public void Action()
        {
            m_isAction = true;
            m_isStart = !m_isStart;
            m_isEnd = !m_isEnd;

            if(m_isStart){ m_startGate.Open(); }
            if (m_isEnd) { m_endGate.Open(); }

        }

        bool IsMoveComleted()
        {
            return m_nowPos == m_startPoint || m_nowPos == m_endPoint;
        }

    }
}