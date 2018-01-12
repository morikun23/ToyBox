using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class EventManager : MonoBehaviour
    {

        //プレイヤ―の取得
        private Player m_player;
        private GameObject m_playerObject;

        //ギミックの取得
        private GameObject m_gimmick;

        //実行するイベント
        protected IEvent m_currentEvent;

        //イベントの状態　　
        enum EventState
        {
            //開始時、実行中、終了時、待機
            START,
            UPDATE,
            END,
            IDLE
        }

        private EventState m_eventState;

        void Start()
        {
            m_eventState = EventState.IDLE;
        }

        void Update()
        {
            ExecCurrentEvent();
        }

        /// <summary>
        /// イベントの実行
        /// </summary>
        private void ExecCurrentEvent()
        {

            switch (m_eventState) {

                case EventState.IDLE:
                    return;

                case EventState.START:

                    //プレイヤーが動けないようにする
                    m_player.m_inputHandle.m_ableRun = false;
                    m_player.m_inputHandle.m_ableJump = false;
                    m_player.m_inputHandle.m_ableReach = false;


                    m_currentEvent.OnStart(m_playerObject,m_gimmick);
                    m_eventState = EventState.UPDATE;
                    break;

                case EventState.UPDATE:

                    
                    bool toNextState = m_currentEvent.OnUpdate();

                    //ここで返されたのがtrueなら次に行く
                    if (toNextState) m_eventState = EventState.END;
                    break;

                case EventState.END:
                    m_currentEvent.OnEnd();
                    m_eventState = EventState.IDLE;

                    m_player.m_inputHandle.m_ableRun = true;
                    m_player.m_inputHandle.m_ableJump = true;
                    m_player.m_inputHandle.m_ableReach = true;

                    break;
            }
        }
        

        /// <summary>
        /// イベントの取得
        /// ギミックから呼び出し
        /// </summary>
        /// <param name="arg_nextEvent">ギミックから呼び出したいイベントをセット</param>
        public void SetEvent(IEvent arg_nextEvent,GameObject arg_gimmick,GameObject arg_playerObject)
        {
            m_gimmick = arg_gimmick;
            m_playerObject = arg_playerObject;

            m_currentEvent = arg_nextEvent;
            m_eventState = EventState.START;


            //初期化関数に置きたいです
            m_player = arg_playerObject.GetComponent<Player>();
            if (m_player == null)
            {
                Debug.LogError("Playerが見つかりません");
            }

        }

    }
    
}