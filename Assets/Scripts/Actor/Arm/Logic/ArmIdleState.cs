using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmIdleState : IArmState {

		/// <summary>
		/// ステート開始時
		/// </summary>
		public void OnEnter(Arm arg_arm) {
			arg_arm.m_currentAngle = 90f;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_arm"></param>
		public void OnUpdate(Arm arg_arm) {

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_arm"></param>
		public void OnExit(Arm arg_arm) {

		}
	}
}