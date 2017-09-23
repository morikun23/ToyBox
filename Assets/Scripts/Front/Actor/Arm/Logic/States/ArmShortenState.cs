using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmShortenState : IArmState {

		//伸ばしきった
		private bool m_finished;

		public void OnEnter(ArmComponent arg_arm) {
			m_finished = false;
		}

		public void OnUpdate(ArmComponent arg_arm) {
			arg_arm.m_currentLength = arg_arm.m_lengthBuf.Pop();

			if(arg_arm.m_lengthBuf.Count <= 0) {
				m_finished = true;
			}
		}

		public void OnExit(ArmComponent arg_arm) {
			arg_arm.m_owner.m_inputHandle.m_reach = false;
		}

		public IArmState GetNextState(ArmComponent arg_arm) {
			if (m_finished) {
				arg_arm.m_isActive = false;
				return new ArmStandByState();
			}
			return null;
		}
	}
}