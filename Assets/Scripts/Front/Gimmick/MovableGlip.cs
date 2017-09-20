using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MovableGlip : FixedItem {

		public override void OnGraspedEnter(PlayerComponent arg_player) {
			base.OnGraspedEnter(arg_player);
			arg_player.Arm.m_shorten = true;
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			ArmComponent arm = arg_player.Arm;

			Vector3 direction = ((Vector2)arm.m_transform.position - arm.m_targetPosition).normalized;

			if (arm.m_lengthBuf.Count > 0) {
				arm.m_transform.position = m_transform.position + (direction * arm.m_lengthBuf.Peek());
			}
			else {
				arm.m_transform.position = m_transform.position;
			}
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
			base.OnGraspedExit(arg_player);
			arg_player.m_rigidbody.isKinematic = false;
			arg_player.m_inputHandle.m_reach = false;
		}

	}
}