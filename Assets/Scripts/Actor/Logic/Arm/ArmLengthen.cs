using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmLengthen : IArmAction {

		private float m_currentLength;
		private float m_range;

		public void OnUpdate(Arm arg_arm) {

			m_currentLength = arg_arm.m_currentLength;
			m_range = arg_arm.m_currentRange;

			if (arg_arm.m_currentLength < arg_arm.m_currentRange) {
				arg_arm.m_currentLength += arg_arm.m_currentRange / 50f;
			}
		}

		public bool IsFinished() {
			return m_currentLength >= m_range;
		}
	}
}