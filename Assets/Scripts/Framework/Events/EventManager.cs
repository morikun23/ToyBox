using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class EventManager : MonoBehaviour
    {

        //プレイヤ―の取得
        private Player m_player;

        //ギミックの取得
        private GameObject m_gimmick;

        //実行するイベント
        protected IEvent m_currentEvent;

		//Mainシーンへの参照
		private MainScene m_mainSceneManager;

		private MainScene MainSceneManager {
			get {
				if (m_mainSceneManager == null) {
					m_mainSceneManager = FindObjectOfType<MainScene>();
				}
				return m_mainSceneManager;
			}
		}

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

					//プレイヤーがUI操作をできないようにする
					AppManager.Instance.user.m_temp.m_isTouchUI = false;


                    m_currentEvent.OnStart(m_player,m_gimmick);
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

					AppManager.Instance.user.m_temp.m_isTouchUI = true;

					break;
            }
        }
        

        /// <summary>
        /// イベントの取得
        /// ギミックから呼び出し
        /// </summary>
        /// <param name="arg_nextEvent">ギミックから呼び出したいイベントをセット</param>
        public void SetEvent(IEvent arg_nextEvent,GameObject arg_gimmick)
        {
            m_gimmick = arg_gimmick;

            m_currentEvent = arg_nextEvent;
            m_eventState = EventState.START;


            //初期化関数に置きたいです
            if (m_player == null)
            {
                m_player = GameObject.FindObjectOfType<Player>();
            }

        }

    }
    
}