using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MainSceneUI : MonoBehaviour {

		


		private Playable m_player;

		public void Initialize(Playable arg_player) {
            
			//m_player = arg_player;
			//m_jumpButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnJumpButtonDown),
			//	new ButtonAction(ButtonEventTrigger.OnRelease,this.OnJumpButtonUp));
			//m_leftButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnLeftButtonDown) ,
			//	new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp));
			//m_rightButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnRightButtonDown) ,
			//	new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp));

			//m_debugButton.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease , this.OnDebugButton));

   //         m_neoButton.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease, this.OnNeoButtonUp));
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