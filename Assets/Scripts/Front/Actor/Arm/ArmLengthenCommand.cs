//担当：森田　勝
//概要：アームを伸ばします
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmLengthenCommand : IArmCommand {
	
		//増加量
		private float m_increase;

		//長さのバッファ
		private float m_lengthBuf;

		/// <summary>
		/// コンストラクタ
		/// 生成時に増加量の決定とバッファを用意する
		/// </summary>
		/// <param name="arg_arm"></param>
		/// <param name="arg_increase"></param>
		public ArmLengthenCommand(Arm arg_arm,float arg_increase) {
			m_lengthBuf = arg_arm.m_currentLength;
			m_increase = arg_increase;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="arg_arm"></param>
		public void Execute(Arm arg_arm) {
			arg_arm.m_currentLength += m_increase;
			if(arg_arm.m_currentLength > arg_arm.m_currentRange) {
				arg_arm.m_currentLength = arg_arm.m_currentRange;
			}
		}

		/// <summary>
		/// 取り消し
		/// 元の長さに戻します
		/// </summary>
		/// <param name="arg_arm"></param>
		public void Undo(Arm arg_arm) {
			arg_arm.m_currentLength = m_lengthBuf;
		}
	}
}