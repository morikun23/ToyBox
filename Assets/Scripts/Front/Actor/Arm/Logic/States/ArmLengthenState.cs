using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmLengthenState : IArmState {

		//増加量
		private float m_increase;

		//伸ばしきった
		private bool m_isFinished;

		//衝突した（中止します）
		private bool m_isCancelled;

		public ArmLengthenState(float arg_increase) {
			m_increase = arg_increase;
		}

		public void OnEnter(ArmComponent arg_arm) {
			m_isFinished = false;
			m_isCancelled = false;
			AppManager.Instance.m_timeManager.Pause();
		}

		public void OnUpdate(ArmComponent arg_arm) {

			if (!arg_arm.m_owner.Hand.m_itemBuffer) {
				//掴むもうとしているターゲットが何らかの理由で消えた場合はすぐに中止
				m_isCancelled = true;
			}

			if (IsCollided(arg_arm)) {
				m_isCancelled = true;
			}

			if (m_isCancelled) return;

			arg_arm.m_lengthBuf.Push(arg_arm.m_currentLength);
			
			if (arg_arm.m_currentLength + m_increase > arg_arm.m_targetLength) {
				arg_arm.m_currentLength = arg_arm.m_targetLength;
				m_isFinished = true;
				arg_arm.m_lengthBuf.Push(arg_arm.m_targetLength);
			}
			else {
				arg_arm.m_currentLength += m_increase;
			}
		}

		public void OnExit(ArmComponent arg_arm) {
			arg_arm.m_owner.Hand.CallResultArmLengthend(m_isFinished);
			AppManager.Instance.m_timeManager.Resume();
		}

		public IArmState GetNextState(ArmComponent arg_arm) {
			

			if (m_isCancelled) {
				return new ArmShortenState();
			}
			if (m_isFinished) {
				return new ArmFreezeState();
			}
			return null;
		}

		private bool IsCollided(ArmComponent arg_arm) {
			Vector2 forward = (arg_arm.m_targetPosition - (Vector2)arg_arm.m_transform.position).normalized;
			if (Physics2D.Raycast(arg_arm.GetTopPosition() , forward , m_increase , 1 << LayerMask.NameToLayer("Ground"))) {
				return true;
			}
			return false;
		}
	}
}