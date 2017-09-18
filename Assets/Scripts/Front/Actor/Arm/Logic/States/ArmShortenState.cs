using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmShortenState : IArmState {

		//伸ばしきった
		private bool m_finished;

		public void OnEnter(Arm arg_arm) {
			m_finished = false;
		}

		public void OnUpdate(Arm arg_arm) {
			arg_arm.m_currentLength = arg_arm.m_lengthBuf.Pop();

			if(arg_arm.m_lengthBuf.Count <= 0) {
				m_finished = true;
			}
		}

		public void OnExit(Arm arg_arm) {

		}

		public IArmState GetNextState(Arm arg_arm) {
			if (m_finished) {
				arg_arm.lengthen = false;
				return new ArmStandByState();
			}
			return null;
		}
	}
}