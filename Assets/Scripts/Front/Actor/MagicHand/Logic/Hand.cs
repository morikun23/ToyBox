//担当：森田　勝
//概要：マジックハンドの部分を制御する
//　　　マジックハンドの仕事は、掴む、握る、放すの動作など
//参考：とくになし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	[System.Serializable]
	public class Hand : ObjectBase {

		private HandViewer m_viewer;

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_arm"></param>
		public void Initialize(Arm arg_arm) {
			m_viewer = GetComponentInChildren<HandViewer>();
			m_viewer.Initialize(this);
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_arm"></param>
		public void UpdateByFrame(Arm arg_arm) {

			m_transform.position = arg_arm.GetTopPosition();

			m_viewer.UpdateByFrame(this);
			m_viewer.SetRotation(arg_arm.m_currentAngle);
		}
		
		public void SetGrabbable() {

		}
	}
}