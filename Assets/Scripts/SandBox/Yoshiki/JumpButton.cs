using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class JumpButton : Button{

        RaycastHit2D m_jumpFlg;

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                OnDown();
            }
            else{
                OnUp();
            }

            bool isGrounded = FindObjectOfType<PlayerComponent>().m_isGrounded;

            //ボタンのアニメーション
            if (!isGrounded)
            {
                m_animator.SetTrigger("Jump");
            }
            else
            {
                m_animator.SetBool("Jump", false);
            }

        }

        public void Initialize()
        {
            m_playable = FindObjectOfType<Playable>();
            m_animator = GetComponent<Animator>();
        }

        void OnDown()
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //タッチをした位置にオブジェクトがあるかどうかを判定
            m_jumpFlg = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("JumpButton"));

            m_playable.m_inputHandle.m_jump = Input.GetMouseButtonDown(0) && m_jumpFlg;

        }

        void OnUp()
        {

        }
    }
}