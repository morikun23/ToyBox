using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class InputManager {
		
		private Playable m_player { get; set; }
		
		private static InputManager m_instance;

		public void Initialize(MainScene arg_mainScene) {
			m_player = arg_mainScene.m_actorManager.m_player;
		}

		public void UpdateByFrame(MainScene arg_mainScene) {

			if (Input.GetMouseButton(0)) {
				m_player.Action4();
			}

			InputKey();
		}

		private void InputKey() {
			#region PCキーボード入力
			if (Input.GetKey(KeyCode.LeftArrow)) {
				m_player.Action1();
			}
			else if (Input.GetKey(KeyCode.RightArrow)) {
				m_player.Action2();
			}
			if (Input.GetKeyDown(KeyCode.Z)) {
				m_player.Action3();
			}
			#endregion
		}

		private void InputSwipe() {
			#region スマホスワイプ入力
			if (Input.touchCount > 0) {
				Touch touchInfo = Input.GetTouch(0);
				if (touchInfo.deltaPosition.x > 10) {
					m_player.Action2();
				}
				if (touchInfo.deltaPosition.x < -10) {
					m_player.Action1();
				}
			}
			#endregion
		}

	}
}