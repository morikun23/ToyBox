using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class EventsManager : MonoBehaviour
    {
        private Player m_player;

        protected EventBase m_currentEvent;

        enum EventState
        {
            Start,Update,End,Idle
        }

        private EventState m_eventState;

        void Start()
        {
            m_player = GameObject.FindObjectOfType<Player>();
            if(m_player == null)
            {
                Debug.Log("Playerが見つかりません");
                return;
            }

            m_eventState = EventState.Idle;

        }

        void Update()
        {
            CurrentEvent();
        }

        void CurrentEvent()
        {

            switch (m_eventState) {

                case EventState.Idle:
                    return;

                case EventState.Start:
                    m_player.m_inputHandle.m_ableRun = false;
                    m_player.m_inputHandle.m_ableJump = false;
                    m_player.m_inputHandle.m_ableReach = false;

                    m_currentEvent.OnStartEvent();
                    m_eventState = EventState.Update;
                    break;

                case EventState.Update:
                    bool m_isflg = m_currentEvent.OnUpdateEvent();
                    if (!m_isflg) m_eventState = EventState.End;
                    break;

                case EventState.End:
                    m_currentEvent.OnEndEvent();
                    m_eventState = EventState.Idle;

                    m_player.m_inputHandle.m_ableRun = true;
                    m_player.m_inputHandle.m_ableJump = true;
                    m_player.m_inputHandle.m_ableReach = true;

                    break;
            }
        }
        
        public void GetEvent(EventBase arg_nextEvent)
        {
            m_currentEvent = arg_nextEvent;
            m_eventState = EventState.Start;
            Debug.Log("呼び出し");
        }

    }
    
}