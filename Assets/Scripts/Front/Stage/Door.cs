using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Door : MonoBehaviour
    {
        EventManager m_eventManager;

        Animator m_animator;
        
        //スタートまたはゴール
        //今のところ意味ないです
        enum DoorType
        {
            Start,
            End
        }

        [SerializeField]
        DoorType m_doorType;

        void Start()
        {
            SetDoorType();
            m_animator = GetComponent<Animator>();
        }
        
        void OnTriggerEnter2D(Collider2D m_hitObject)
        {
            string layerName = LayerMask.LayerToName(m_hitObject.gameObject.layer);

            if (IsDoorHit(layerName))
            {
                GameObject.FindObjectOfType<EventManager>().SetEvent(new ClearEvent(),gameObject);
            }
        }


        //ゲームが始まったときの処理
        void SetDoorType()
        {
            if(m_doorType == DoorType.Start)
            {
                
            }
            else if(m_doorType == DoorType.End)
            {
                
            }
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        /// <param name="arg_layerName"></param>
        /// <returns></returns>
        bool IsDoorHit(string arg_layerName)
        {
            return m_doorType == DoorType.End && arg_layerName == "Player";
        }

        /// <summary>
        /// ドアを開く
        /// </summary>
        public void OpenDoor()
        {
            m_animator.Play("Open");
        }

        /// <summary>
        /// ドアが閉まるとき
        /// </summary>
        public void EnterCloseDoor()
        {
            m_animator.Play("Close");
            
        }

        //ドアが閉まったかどうか
        public bool isAnimationCloseDoor()
        {
            AnimatorStateInfo stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime > 1)
            {
                return false;
            }
            return true;
        }


    }
}
