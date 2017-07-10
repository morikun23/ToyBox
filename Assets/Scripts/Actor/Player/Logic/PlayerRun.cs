using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerRun : IPlayerState {
		
		public void OnEnter(Player arg_player) {

		}

		public void OnUpdate(Player arg_player) {
			if (Input.GetKey((KeyCode.RightArrow))) {
				arg_player.m_position += Vector2.right * 0.1f;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				arg_player.m_position += Vector2.left * 0.1f;
			}
			if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
				arg_player.StateTransition(new PlayerIdle());
			}
		}

		public void OnExit(Player arg_player) {

		}
		
	}
}