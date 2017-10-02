//担当：森田　勝
//概要：プレイヤーの死亡状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerDeadState : PlayerStateBase {

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

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnExit(Player arg_player) {
			arg_player.m_task.Clear();
		}

		public override void AddTaskIfAble(Player arg_player, IPlayerCommand arg_command) {
			return;
		}
	}
}