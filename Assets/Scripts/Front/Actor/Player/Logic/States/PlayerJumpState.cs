//担当：森田　勝
//概要：プレイヤーのジャンプ状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerJumpState : PlayerStateBase {

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
					arg_player.m_task.Dequeue().Execute(arg_player);
				}
			}
			if (arg_player.m_isGrounded) {
				if (arg_player.m_rigidbody.velocity.y < 0) {
					arg_player.StateTransition(new PlayerIdleState());
				}
			}
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnExit(Player arg_player) {
			
		}

		public override void AddTaskIfAble(Player arg_player, IPlayerCommand arg_command) {
			if(arg_command.GetType() == typeof(PlayerRunLeftCommand)) {
				arg_player.m_task.Enqueue(arg_command);
			}
			else if (arg_command.GetType() == typeof(PlayerRunRightCommand)) {
				arg_player.m_task.Enqueue(arg_command);
			}
			else if (arg_command.GetType() == typeof(PlayerReachCommand)) {
				arg_player.m_task.Enqueue(arg_command);
				arg_player.StateTransition(new PlayerReachState());
			}
		}
	}
}