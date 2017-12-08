using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MoveButton : Button {

		[SerializeField]
		private ActorBase.Direction m_direction;

//		public override void Initialize() {
//			base.Initialize();
//		}

//		public override void UpdateByFrame() {
//
//			base.UpdateByFrame();

#if UNITY_EDITOR && DEVELOP
			if (Input.GetKeyDown(m_key)) {
				this.OnDown();
				return;
			}
			if (Input.GetKey(m_key)) {
				this.OnPress();
			}
			if (Input.GetKeyUp(m_key)) {
				this.OnUp();
			}
#endif
//		}

		public override void OnDown() {
			base.OnDown();
		}

		public override void OnPress() {
			base.OnPress();
			InputManager.Instance.GetPlayableCharactor ().m_direction = m_direction;
			InputManager.Instance.GetPlayableCharactor ().m_inputHandle.m_run = true;
		}

		public override void OnUp() {
			base.OnUp();

			InputManager.Instance.GetPlayableCharactor ().m_inputHandle.m_run = false;
		}

	}
}