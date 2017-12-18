using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MainSceneUI : CommonUI {

        [SerializeField]
        UIButton m_jumpButton;

        [SerializeField]
        UIButton m_leftButton;
        [SerializeField]
        UIButton m_rightButton;

        void Start()
        {
            m_jumpButton.Initialize(JumpButton,"反応");

            m_leftButton.Initialize(LeftButton);
            m_rightButton.Initialize(RightButton);
        }

        void JumpButton(object arg_jump)
        {
            InputManager.Instance.GetPlayableCharactor().m_inputHandle.m_jump = true;
            Debug.Log(arg_jump);
        }

        void LeftButton()
        {
            InputManager.Instance.GetPlayableCharactor().m_direction = ActorBase.Direction.LEFT;
            InputManager.Instance.GetPlayableCharactor().m_inputHandle.m_run = true;
        }

        void RightButton()
        {
            InputManager.Instance.GetPlayableCharactor().m_direction = ActorBase.Direction.RIGHT;
            InputManager.Instance.GetPlayableCharactor().m_inputHandle.m_run = true;
        }


    }
}