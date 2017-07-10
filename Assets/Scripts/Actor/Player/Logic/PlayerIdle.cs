using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerIdle : IPlayerState {

		public void OnEnter(Player arg_player) {
			
		}

		public void OnUpdate(Player arg_player) {
			if (Input.GetKey((KeyCode.RightArrow))) {
				arg_player.StateTransition(new PlayerRun());
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				arg_player.StateTransition(new PlayerRun());
			}
		}

		public void OnExit(Player arg_player) {

		}
	}
}