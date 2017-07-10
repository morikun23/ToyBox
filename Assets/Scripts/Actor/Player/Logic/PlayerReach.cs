using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerReach : IPlayerState {

		public void OnEnter(Player arg_player) {
			//arg_player.m_arm.AddTask(new ArmLengthen());
			//arg_player.m_magicHand.AddTask(new MagicHandGrab());
			//arg_player.m_magicHand.AddTask(new MagicHandGrasp());
		}

		public void OnUpdate(Player arg_player) {
			//TODO:Aimに移動
			//arg_player.m_arm.Rotate(-Input.GetAxis("Horizontal") * 2f);
		}

		public void OnExit(Player arg_player) {

		}
	}
}