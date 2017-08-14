//担当：森田　勝
//概要：マジックハンドの部分を制御する
//　　　マジックハンドの仕事は、掴む、握る、放すの動作など
//参考：とくになし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	[System.Serializable]
	public class Hand : AnimationObject {

		public Queue<IHandCommand> m_task { get; private set; }

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_arm"></param>
		public void Initialize(Arm arg_arm) {
			m_task = new Queue<IHandCommand>();
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_arm"></param>
		public void UpdateByFrame(Arm arg_arm) {
			

		}
		/// <summary>
		/// アームの向きに合わせて角度を調整する
		/// </summary>
		/// <param name="arg_angle">角度</param>
		public void SetRotation(float arg_angle) {
			m_transform.eulerAngles = Vector3.forward * arg_angle;
		}
	}
}