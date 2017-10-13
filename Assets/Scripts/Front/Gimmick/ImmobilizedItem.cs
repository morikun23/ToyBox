using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ImmobilizedItem : Item {

		public override void OnGraspedEnter(PlayerComponent arg_player) {
			
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
			arg_player.Arm.m_shorten = true;
		}
	}
}