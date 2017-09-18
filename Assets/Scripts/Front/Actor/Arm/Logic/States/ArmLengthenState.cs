using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmLengthenState : IArmState {

		//増加量
		private float m_increase;

		//伸ばしきった
		private bool m_finished;

		//衝突した
		private bool m_isCollided;

		public ArmLengthenState(float arg_increase) {
			m_increase = arg_increase;
		}

		public void OnEnter(Arm arg_arm) {
			m_finished = false;
		}

		public void OnUpdate(Arm arg_arm) {

			m_isCollided = IsCollided(arg_arm);

			if (m_isCollided) return;

			arg_arm.m_lengthBuf.Push(arg_arm.m_currentLength);
			
			if (arg_arm.m_currentLength + m_increase > arg_arm.m_targetLength) {
				arg_arm.m_currentLength = arg_arm.m_targetLength;
				m_finished = true;
			}
			else {
				arg_arm.m_currentLength += m_increase;
			}
		}

		public void OnExit(Arm arg_arm) {
			
		}

		public IArmState GetNextState(Arm arg_arm) {
			if (m_isCollided) {
				return new ArmShortenState();
			}
			if (m_finished) {
				return new ArmFreezeState();
			}
			return null;
		}

		private bool IsCollided(Arm arg_arm) {
			Vector2 forward = (arg_arm.m_targetPosition - (Vector2)arg_arm.m_transform.position).normalized;
			if (Physics2D.Raycast(arg_arm.GetTopPosition() , forward , m_increase , 1 << LayerMask.NameToLayer("Ground"))) {
				return true;
			}
			return false;
		}
	}
}