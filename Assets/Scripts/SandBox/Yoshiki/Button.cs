 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class Button : ActorBase
    {
        [SerializeField]
        GameObject m_jump;
        [SerializeField]
        GameObject m_left;
        [SerializeField]
        GameObject m_right;

        RaycastHit2D jumpFlg;
        RaycastHit2D rightFlg;
        RaycastHit2D leftFlg;

        private Playable m_playable;

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            UpdateByFrame();
        }

        public void Initialize()
        {
            m_playable = FindObjectOfType<Playable>();
        }

        public void UpdateByFrame()
        {

            if (Input.GetMouseButton(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //タッチをした位置にオブジェクトがあるかどうかを判定
                jumpFlg = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("JumpButton"));
                rightFlg = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("RightButton"));
                leftFlg = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("LeftButton"));

                if (leftFlg)
                {
                    m_playable.m_direction = Direction.LEFT;
                }
                if (rightFlg)
                {
                    m_playable.m_direction = Direction.RIGHT;
                }

            }

            m_playable.m_inputHandle.m_run = Input.GetMouseButton(0) && (leftFlg || rightFlg);

            m_playable.m_inputHandle.m_jump = Input.GetMouseButton(0) && jumpFlg;
        }

    }
}