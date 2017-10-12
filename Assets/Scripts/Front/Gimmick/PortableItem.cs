using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PortableItem : Item {

		public override void OnGraspedEnter(PlayerComponent arg_player) {
			arg_player.Arm.m_shorten = true;
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			this.m_transform.position = arg_player.Arm.GetTopPosition();
			if (arg_player.GetCurrentState() != typeof(PlayerReachState)) {
				arg_player.m_rigidbody.isKinematic = false;
			}
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
			
			arg_player.m_inputHandle.m_reach = false;
		}
	}
}