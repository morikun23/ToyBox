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

        //開閉口
        private ElevatorGate m_startGate;
        private ElevatorGate m_endGate;
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


        //イベントマネージャができるまで
        GameObject m_player;
        Vector3 m_playerRidePos;

        void Start()
        {
            
            #region 各種オブジェの参照 
            //開始位置・終了位置の取得
            m_startPoint = transform.Find("StartPoint").transform.position;
            m_endPoint = transform.Find("EndPoint").transform.position;

            //開閉口の取得
            m_startGate = transform.Find("StartPoint").transform.Find("StartGate").GetComponent<ElevatorGate>();
            m_endGate = transform.Find("EndPoint").transform.Find("EndGate").GetComponent<ElevatorGate>();

            //エレベーターの当たり判定の取得
            m_collider = transform.Find("Collider").gameObject;
            m_collider.transform.position = m_startPoint;

            #endregion

            //初期位置と目標位置の初期化
            m_nowPos = m_startPoint;
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
            //動いてなければ戻る
            if (!m_isAction) return;

            //往復移動
            m_nowPos = Vector2.MoveTowards(m_collider.transform.position, m_targetPos, m_moveNum);

            //イベントマネージャができるまで
            //プレイヤーを動けないようにする
            m_player.transform.position = new Vector2(m_playerRidePos.x, m_nowPos.y + 0.65f);


            if (IsMoveComleted())
            {             


                //スタート地点向きかどうか
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
                //往復移動を続ける
                m_rigidbody.MovePosition(m_nowPos);
            }
        }

        /////<summary>
        /////外部から作動させたい場合に
        /////</summary> 
        //public void Action()
        //{
        //    m_isAction = true;
        //    m_isStart = !m_isStart;
        //    m_isEnd = !m_isEnd;

        //    if (m_isStart) { m_startGate.Open(); }
        //    if (m_isEnd) { m_endGate.Open(); }

        //}


        ///<summary>
        ///外部から作動させたい場合に(イベントマネージャができるまで)
        ///</summary> 
        public void Action()
        {
            m_isAction = true;

            //プレイヤー情報の取得
            m_player = GameObject.Find("Player");
            m_playerRidePos = m_player.transform.position;
            m_player.transform.position = new Vector2(m_playerRidePos.x, m_nowPos.y + 0.65f);

            m_isStart = !m_isStart;
            m_isEnd = !m_isEnd;

            if(m_isStart){ m_startGate.Open(); }
            if (m_isEnd) { m_endGate.Open(); }

        }


        ///<summary>
        ///目的地に到達した場合
        ///</summary> 
        bool IsMoveComleted()
        {
            return m_nowPos == m_startPoint || m_nowPos == m_endPoint;
        }

    }
}