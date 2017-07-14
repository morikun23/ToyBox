//担当：森田　勝
//概要：アームが短くなる動きを実装したもの
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmShorten : IArmAction {

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_arm">更新をかけるアーム</param>
		public void OnUpdate(Arm arg_arm) {

			//長さの最小値
			const int MIN = 0;

			//減少量
			float decrease = arg_arm.m_currentRange / 50f;

			if (arg_arm.m_currentLength - decrease >= MIN) {
				arg_arm.m_currentLength -= decrease;
			}
			else {
				arg_arm.m_currentLength = MIN;
			}
		}

	}
}