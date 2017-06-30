using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerAim : IPlayerState {
		
		public void OnEnter(Player arg_player) {
			
		}

		public void OnUpdate(Player arg_player) {
			//TODO:トリガーの変更
			//決定ボタンから通知させる
			if (Input.GetKeyDown(KeyCode.Space)) {
				//TODO:MagicHandが必要
				arg_player.StateTransition(new PlayerReach());
			}
		}

		public void OnExit(Player arg_player) {

		}
	}
}