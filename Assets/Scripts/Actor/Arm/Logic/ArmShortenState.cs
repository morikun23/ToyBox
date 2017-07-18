//担当：森田　勝
//概要：アームが短くなる動きを実装したもの
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmShortenState : IArmState {

		//キーフレームで動かすためのフレームレート
		private const int RATE = 20;

		//増加量
		private float m_decrease;

		//長さの最小値
		const int MIN = 0;

		/// <summary>
		/// ステート開始時
		/// </summary>
		public void OnEnter(Arm arg_arm) {
			float lengthTargetToBottom = (arg_arm.GetTopPosition() - arg_arm.GetBottomPosition()).magnitude;

			m_decrease = lengthTargetToBottom / RATE;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_arm">更新をかけるアーム</param>
		public void OnUpdate(Arm arg_arm) {
			
			if (arg_arm.m_currentLength - m_decrease >= MIN) {
				arg_arm.m_currentLength -= m_decrease;
			}
			else {
				arg_arm.m_currentLength = MIN;
				arg_arm.StateTransition(new ArmIdleState());
			}
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_arm"></param>
		public void OnExit(Arm arg_arm) {

		}
	}
}