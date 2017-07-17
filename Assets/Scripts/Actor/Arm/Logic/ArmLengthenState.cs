//担当：森田　勝
//概要：アームが長くなる動きを実装したもの
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmLengthen : IArmState {

		//キーフレームで動かすためのフレームレート
		private const int RATE = 20;

		//増加量
		private float m_increase;

		//射程
		float m_range;

		/// <summary>
		/// ステート開始時
		/// </summary>
		public void OnEnter(Arm arg_arm) {

			arg_arm.m_currentAngle = GetRotation(arg_arm.GetBottomPosition() , arg_arm.m_targetPosition);

			//タップされた座標からアームの根元までの長さ
			float lengthTargetToBottom = (arg_arm.m_targetPosition - arg_arm.GetBottomPosition()).magnitude;
			
			//現在の射程より遠いところへタップした場合に修正させる
			if (lengthTargetToBottom < arg_arm.m_currentRange) { m_range = lengthTargetToBottom; }
			else { m_range = arg_arm.m_currentRange; }

			//増加量をキーフレームの分だけ決定する
			m_increase = 0.5f;
			
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_arm">更新をかけるアーム</param>
		public void OnUpdate(Arm arg_arm) {

			//射程に達していない場合に限り、長さを加える
			if (arg_arm.m_currentLength + m_increase <= m_range) {
				arg_arm.m_currentLength += m_increase;
				
			}
			else {
				//伸ばし切っていたら固定状態へ
				arg_arm.m_currentLength = m_range;
				arg_arm.StateTransition(new ArmFreezeState());
			}
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_arm"></param>
		public void OnExit(Arm arg_arm) {

		}

		/// <summary>
		/// 角度を反映する
		/// </summary>
		private float GetRotation(Vector2 arg_from , Vector2 arg_to) {
			float y = arg_to.y - arg_from.y;
			float x = arg_to.x - arg_from.x;

			float theta = Mathf.Atan2(y , x);

			return theta * 180 / Mathf.PI;
		}

	}
}