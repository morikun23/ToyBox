//担当：森田　勝
//概要：アームとしての振る舞いを実行するクラス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class Arm : ArmComponent {
		
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_player"></param>
		public void Initialize() {
			
			m_currentState = new ArmStandByState();
			m_lengthBuf = new Stack<float>();
			m_currentLength = 0f;
			
			m_viewer = GetComponentInChildren<ArmViewer>();
			m_viewer.Initialize(this);
			
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void UpdateByFrame() {

			//プレイヤーの座標に依存する
			m_transform.position = m_owner.transform.position;

			IArmState nextState = m_currentState.GetNextState(this);
			if(nextState != null) {
				StateTransition(nextState);
			}
			m_currentState.OnUpdate(this);
			
			m_viewer.UpdateByFrame(this);
		}
		
	}
}