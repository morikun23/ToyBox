using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MainSceneUI : MonoBehaviour {

		[Header("Buttons")]
		[SerializeField]
		UIButton m_jumpButton;
		
		[SerializeField]
		UIButton m_leftButton;

		[SerializeField]
		UIButton m_rightButton;

		[SerializeField]
		UIButton m_debugButton;

        [SerializeField]
        UIButton m_neoButton;


		private Playable m_player;

		public void Initialize(Playable arg_player) {
            
			m_player = arg_player;
			m_jumpButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnJumpButtonDown),
				new ButtonAction(ButtonEventTrigger.OnRelease,this.OnJumpButtonUp));
			m_leftButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnLeftButtonDown) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp));
			m_rightButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnRightButtonDown) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp));

			m_debugButton.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease , this.OnDebugButton));

            m_neoButton.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease, this.OnNeoButtonUp));
		}
		
		private void OnJumpButtonDown() {
            if(m_player.m_inputHandle.m_ableJump)
			m_player.m_inputHandle.m_jump = true;
		}

		private void OnJumpButtonUp() {
			m_player.m_inputHandle.m_jump = false;
		}

		private void OnLeftButtonDown() {
            if (m_player.m_inputHandle.m_ableRun)
            {
                m_player.m_direction = ActorBase.Direction.LEFT;
                m_player.m_inputHandle.m_run = true;
            }
		}

		private void OnMoveButtonUp() {
			m_player.m_inputHandle.m_run = false;
		}

		private void OnRightButtonDown() {
            if (m_player.m_inputHandle.m_ableRun)
            {
                m_player.m_direction = ActorBase.Direction.RIGHT;
                m_player.m_inputHandle.m_run = true;
            }
		}


		int count = 0;
		private void OnDebugButton() {
			count += 1;
			if(count >= 10) {
				Stage stage = FindObjectOfType<Stage>();
				if (stage) {
					stage.StashData();
					UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
				}
			}
		}
        private void OnNeoButtonUp()
        {
            if (m_player.m_inputHandle.m_ableReach)
            {
                m_player.m_inputHandle.m_ableReach = true;
            }
        }
    }
}