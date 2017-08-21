//担当：森田　勝
//概要：プレイヤーの移動状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerRunState : PlayerStateBase {

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnEnter(Player arg_player) {

		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnUpdate(Player arg_player) {
			if (arg_player.m_task.Count > 0) {
				for (int i = 0; i < arg_player.m_task.Count; i++) {
					Debug.Log(arg_player.m_task.Peek());
					arg_player.m_task.Dequeue().Execute(arg_player);
				}
			}
			else {
				arg_player.StateTransition(new PlayerIdleState());
			}
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnExit(Player arg_player) {
			
		}

	}
}