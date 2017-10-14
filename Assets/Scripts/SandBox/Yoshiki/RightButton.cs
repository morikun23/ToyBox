using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class RightButton : Button
    {
        RaycastHit2D m_rightFlg;

        bool m_pressFlg;

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            if (Input.GetMouseButton(0) && !m_pressFlg)
            {

                OnDown();
                Debug.Log(m_pressFlg);
            }
            else if (!Input.GetMouseButton(0))
            {
                m_pressFlg = false;
                OnUp();
                Debug.Log(m_pressFlg);
            }

            if (m_pressFlg)
            {
                OnPress();

            }

        }

        public void Initialize()
        {
            m_playable = FindObjectOfType<Playable>();
            m_animator = GetComponent<Animator>();
        }
        void OnDown()
        {
            m_pressFlg = true;
        }

        void OnPress()
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //タッチをした位置にオブジェクトがあるかどうかを判定
            m_rightFlg = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("RightButton"));

            if (m_rightFlg)
            {
                m_playable.m_direction = ActorBase.Direction.RIGHT;
                m_playable.m_inputHandle.m_run = true;

            }
            m_animator.SetBool("Right", m_rightFlg);
            


        }
        void OnUp()
        {
            m_playable.m_inputHandle.m_run = false;
            m_animator.SetBool("Right", false);

        }

    }
}