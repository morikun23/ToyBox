using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Door : MonoBehaviour
    {
        EventManager m_eventManager;

        Animator m_animator;

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
        
        void Update()
        {
            
        }

        void OnTriggerEnter2D(Collider2D m_hitObject)
        {
            string layerName = LayerMask.LayerToName(m_hitObject.gameObject.layer);

            if (IsDoorHit(layerName))
            {
                GameObject.FindObjectOfType<EventManager>().SetEvent(new ClearEvent(),gameObject);
            }
        }

        void SetDoorType()
        {
            if(m_doorType == DoorType.Start)
            {
                
            }
            else if(m_doorType == DoorType.End)
            {
                
            }
            else
            {
                Debug.LogError("タイプが不明です");
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

        public void OpenDoor()
        {
            m_animator.Play("Open");
        }

        public void CloseDoor()
        {
            m_animator.Play("Close");
            
        }

        
    }
}
