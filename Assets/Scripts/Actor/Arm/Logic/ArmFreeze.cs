//担当：森田　勝
//概要：アームが固定している動きを実装したもの
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class ArmFreeze : IArmAction {

		/// <summary>
		/// 更新（実際には固定しているので動きなし）
		/// </summary>
		/// <param name="arg_arm"></param>
		public void OnUpdate(Arm arg_arm) {

		}
	}
}