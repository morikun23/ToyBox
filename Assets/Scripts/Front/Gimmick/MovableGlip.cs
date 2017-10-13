using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MovableGlip : FixedItem {

		private Vector3 m_direction;

		public enum GripState{
			Neutoral,
			ENTER,
			STAY,
			EXIT
		}
		public GripState m_enu_state;

		public override void OnGraspedEnter(PlayerComponent arg_player) {
#if DEVELOP
			Debug.Log("Grasped : ENTER");
#endif
			base.OnGraspedEnter(arg_player);
			arg_player.Arm.m_shorten = true;
			m_direction = ((Vector2)arg_player.Arm.m_transform.position - arg_player.Arm.m_targetPosition).normalized;
			arg_player.m_rigidbody.isKinematic = true;
			m_enu_state = GripState.ENTER;
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {

#if DEVELOP
			Debug.Log("Grasped : Update");
#endif

			ArmComponent arm = arg_player.Arm;

			if (arm.m_lengthBuf.Count > 0) {
				arm.m_transform.position = m_transform.position + (m_direction * arm.m_lengthBuf.Peek());
			}
			else {
				m_enu_state = GripState.STAY;
				arm.m_transform.position = m_transform.position;
				arg_player.gameObject.transform.position = arm.m_transform.position = transform.position;
			}
			arg_player.m_rigidbody.velocity = Vector2.zero;
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {

#if DEVELOP
			Debug.Log("Grasped : Exit");
#endif
			m_enu_state = GripState.Neutoral;
			base.OnGraspedExit(arg_player);
			arg_player.m_rigidbody.isKinematic = false;
			arg_player.m_inputHandle.m_reach = false;

		}

	}
}