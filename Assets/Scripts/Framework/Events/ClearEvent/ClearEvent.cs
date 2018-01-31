using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class ClearEvent : IEvent
    {
        
        private Player m_player;
        private Player.Direction m_currentDirection;
        private Rigidbody2D m_rigidbody;
        private Animator m_animator;

        private GameObject m_eventPlayer;
        private Animator m_eventAnimation;
        AnimatorStateInfo m_eventAnimationInfo;

        private GameObject m_doorObject;
        private Door m_door;

        private int m_countJump;

        enum ClearEventState
        {
            STANBY,
            RUN,
            ANIMETION,
            OPEN,
            FADE,
            CLOSE,
            END
        }

        ClearEventState m_clearState;

        public virtual void OnStart(Player arg_player,GameObject arg_gimmickObject)
        {
            m_player = arg_player;
            m_rigidbody = m_player.RigidbodyComponent;
            m_animator = m_player.AnimatorComponent;
            
            m_doorObject = arg_gimmickObject;
            m_door = m_doorObject.GetComponent<Door>();

            m_eventPlayer = m_doorObject.transform.Find("EventAnimation").gameObject;
            m_eventAnimation = m_eventPlayer.GetComponent<Animator>();

            m_clearState = ClearEventState.STANBY;
        }


        public virtual bool OnUpdate()
        {
            switch (m_clearState)
            {
                case ClearEventState.STANBY:

                    m_animator.SetBool("Run",true);
                    
                    m_clearState = ClearEventState.RUN;

                    break;

                case ClearEventState.RUN:

                    
                    m_rigidbody.velocity = new Vector3()
                    {
                        x = 3 * (int)GetPlayerDirection(),
                        y = m_rigidbody.velocity.y,
                        z = 0
                    };

                    if (PositionCheck())
                    {
                        AudioManager.Instance.ReleaseSE("Foot");

                        m_rigidbody.velocity = new Vector3()
                        {
                            x = 0,
                            y = m_rigidbody.velocity.y,
                            z = 0
                        };
                        m_player.transform.position = new Vector3()
                        {
                            x = m_doorObject.transform.position.x,
                            y = m_player.transform.position.y,
                            z = m_player.transform.position.z

                        };

                        m_player.Stop(m_currentDirection);
                        m_animator.SetBool("Run", false);

                        m_player.gameObject.active = false;
                        m_eventAnimation.gameObject.active = true;

                        m_eventAnimation.Play("ANM_ClearEvent");
                        m_clearState = ClearEventState.ANIMETION;
                    }

                    break;

                case ClearEventState.ANIMETION:
                    
                    m_eventAnimationInfo = m_eventAnimation.GetCurrentAnimatorStateInfo(0);

                    if (m_eventAnimationInfo.normalizedTime > 1.0f)
                    {
                        m_clearState = ClearEventState.OPEN;
                    }
                    break;
                case ClearEventState.OPEN:
                    m_door.OpenDoor();
                    m_eventAnimation.Play("ANM_ClearEventFade");
                    m_clearState = ClearEventState.FADE;
                    break;
                case ClearEventState.FADE:

                    m_eventAnimationInfo = m_eventAnimation.GetCurrentAnimatorStateInfo(0);

                    if (m_eventAnimationInfo.normalizedTime > 1.0f)
                    {
                        m_clearState = ClearEventState.CLOSE;
                    }
                    break;
                case ClearEventState.CLOSE:
                    m_door.CloseDoor();
                    m_clearState = ClearEventState.END;
                    break;
                case ClearEventState.END:
                    break;
            }
            //ここで返されたのがtrueならイベント終了
            if (m_clearState == ClearEventState.END)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }


        public virtual void OnEnd()
        {
            Debug.Log("イベント終了時");
            OnEnterFade();
        }

        Player.Direction GetPlayerDirection()
        {
            if (m_player.transform.position.x < m_doorObject.transform.position.x)
            {
                return Player.Direction.RIGHT;
            }
            else
            {
                return Player.Direction.LEFT;
            }
        }

        bool PositionCheck()
        {
            if (m_player.transform.position.x > m_doorObject.transform.position.x - 0.1f
                && m_player.transform.position.x < m_doorObject.transform.position.x + 0.1f) 
            {
                return true;
            }
            return false;
        }

        void OnEnterFade()
        {

            Fade fade = AppManager.Instance.m_fade;
            fade.StartFade(new FadeOut(), Color.white, 1.0f);
        }

    }
}