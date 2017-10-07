//担当：森田　勝
//概要：マジックハンドの部分を制御する
//　　　マジックハンドの仕事は、掴む、握る、放すの動作など
//参考：とくになし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class Hand : HandComponent {

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			m_viewer = GetComponentInChildren<HandViewer>();
			m_viewer.Initialize(this);
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame() {

			m_transform.position = m_owner.Arm.GetTopPosition();

			if (m_graspingItem) {
				m_graspingItem.OnGraspedStay(m_owner);
			}

			m_viewer.UpdateByFrame(this);
			m_viewer.SetRotation(m_owner.Arm.m_currentAngle);
		}

	}
}