using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmShorten : IArmAction {

		private float m_currentLength;
		private const float MIN = 0;

		public void OnUpdate(Arm arg_arm) {

			m_currentLength = arg_arm.m_currentLength;
			
			if (arg_arm.m_currentLength > MIN) {
				arg_arm.m_currentLength -= arg_arm.m_currentRange / 50f;
			}
		}
		
		public bool IsFinished() {
			return m_currentLength < MIN;
		}

	}
}