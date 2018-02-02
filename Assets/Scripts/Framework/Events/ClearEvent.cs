using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class ClearEvent : IEvent
    {
        
        //プレイヤー関連
        private Player m_player;
        private Player.Direction m_currentDirection;
        private Rigidbody2D m_rigidbody;
        private Animator m_animator;

        //イベントのアニメーション関連
        private GameObject m_eventPlayer;
        private Animator m_eventAnimator;
        AnimatorStateInfo m_eventAnimatorInfo;

        //ゴールのドア
        private GameObject m_doorObject;
        private Door m_door;

        //ジャンプした回数
        private int m_countJump;

        //イベントの進行状態
        enum ClearEventState
        {
            STANBY,
            RUN,
            ANIMETION,
            OPEN,
            PLAYERFADE,
            CLOSE,
            END
        }

        ClearEventState m_clearState;


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="arg_player">プレイヤーの情報</param>
        /// <param name="arg_gimmickObject">ドアの情報</param>
        public virtual void OnStart(Player arg_player,GameObject arg_gimmickObject)
        {
            m_player = arg_player;
            m_rigidbody = m_player.RigidbodyComponent;
            m_animator = m_player.AnimatorComponent;
            
            m_doorObject = arg_gimmickObject;
            m_door = m_doorObject.GetComponent<Door>();

            m_eventPlayer = m_doorObject.transform.Find("ClearAnimation").gameObject;
            m_eventAnimator = m_eventPlayer.GetComponent<Animator>();

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
                    RunState();
                    break;

                case ClearEventState.ANIMETION:
                    m_eventAnimatorInfo = m_eventAnimator.GetCurrentAnimatorStateInfo(0);
                    if (m_eventAnimatorInfo.normalizedTime > 1.0f)
                    {
                        m_clearState = ClearEventState.OPEN;
                    }
                    break;

                case ClearEventState.OPEN:
                    m_door.OpenDoor();
                    m_eventAnimator.Play("ANM_ClearEventFade");
                    m_clearState = ClearEventState.PLAYERFADE;
                    break;

                case ClearEventState.PLAYERFADE:
                    m_eventAnimatorInfo = m_eventAnimator.GetCurrentAnimatorStateInfo(0);
                    if (m_eventAnimatorInfo.normalizedTime > 1.0f)
                    {
                        m_door.EnterCloseDoor();
                        m_clearState = ClearEventState.CLOSE;
                    }
                    break;

                case ClearEventState.CLOSE:
                    if (m_door.isAnimationCloseDoor())
                    {
                        m_clearState = ClearEventState.END;
                    }
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
            //ゲーム終了の処理
        }

        /// <summary>
        /// プレイヤーがドアに対してどっちにいるか
        /// </summary>
        /// <returns></returns>
        private Player.Direction GetPlayerDirection()
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

        /// <summary>
        /// プレイヤーが一定範囲内に入っているか
        /// </summary>
        /// <returns></returns>
        private bool PositionCheck()
        {
            if (m_player.transform.position.x > m_doorObject.transform.position.x - 0.1f
                && m_player.transform.position.x < m_doorObject.transform.position.x + 0.1f) 
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///プレイヤーの位置調整 
        /// </summary>
        private void RunState()
        {
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
                m_eventAnimator.gameObject.active = true;

                m_eventAnimator.Play("ANM_ClearEvent");
                m_clearState = ClearEventState.ANIMETION;
            }
        }

    }
}