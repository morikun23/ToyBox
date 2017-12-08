using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class JumpButton : Button {

//		public override void Initialize() {
//			base.Initialize();
//		}

//		public override void UpdateByFrame() {
//			base.UpdateByFrame();
//
//			m_animator.SetBool("Press" , !m_playable.m_isGrounded);

#if UNITY_EDITOR && DEVELOP
			if (Input.GetKeyDown(m_key)) {
				OnDown();
			}
			if (Input.GetKeyUp(m_key)) {
				OnUp();
			}
#endif
//		}

		public override void OnDown() {
			base.OnDown();
			InputManager.Instance.GetPlayableCharactor ().m_inputHandle.m_jump = true;
			m_animator.SetBool("Press" , true);
		}

		public override void OnPress() {
			base.OnPress();
		}

		public override void OnUp() {
			base.OnUp();
			InputManager.Instance.GetPlayableCharactor ().m_inputHandle.m_jump = false;
		}
	}
}