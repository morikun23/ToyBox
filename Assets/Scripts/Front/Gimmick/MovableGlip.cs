﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MovableGlip : FixedItem {

		private Vector3 m_direction;

		public override void OnGraspedEnter(PlayerComponent arg_player) {
			base.OnGraspedEnter(arg_player);
			arg_player.Arm.m_shorten = true;
			m_direction = ((Vector2)arg_player.Arm.m_transform.position - arg_player.Arm.m_targetPosition).normalized;

		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			ArmComponent arm = arg_player.Arm;

			if (arm.m_lengthBuf.Count > 0) {
				arm.m_transform.position = m_transform.position + (m_direction * arm.m_lengthBuf.Peek());
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