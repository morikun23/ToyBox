//担当：森田　勝
//概要：アームが長くなる動きを実装したもの
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmLengthen : IArmAction {

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_arm">更新をかけるアーム</param>
		public void OnUpdate(Arm arg_arm) {

			//長さの最大値
			float MAX = arg_arm.m_currentRange;

			//増加量
			float increase = arg_arm.m_currentRange / 50f;

			if (arg_arm.m_currentLength + increase <= MAX) {
				arg_arm.m_currentLength += increase;
			}
			else {
				arg_arm.m_currentLength = MAX;
			}
		}
	}
}