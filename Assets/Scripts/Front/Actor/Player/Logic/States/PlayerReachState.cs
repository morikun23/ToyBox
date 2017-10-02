//担当：森田　勝
//概要：プレイヤーの射出状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerReachState : PlayerStateBase {

		private IPlayerCommand m_commandBuf;

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnEnter(Player arg_player) {
			m_commandBuf = arg_player.m_task.Peek();
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnUpdate(Player arg_player) {
			if (arg_player.m_task.Count > 0) {
				for(int i = 0; i < arg_player.m_task.Count; i++) {
					arg_player.m_task.Dequeue().Execute(arg_player);
				}
			}
			else if (arg_player.m_arm.IsShotened()) {
				arg_player.m_task.Enqueue(m_commandBuf);
				arg_player.m_task.Dequeue().Undo(arg_player);
				arg_player.StateTransition(new PlayerIdleState());
			}

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnExit(Player arg_player) {
			arg_player.m_task.Clear();
		}

		protected override bool IsAbleRun() {
			return false;
		}

		protected override bool IsAbleReach() {
			return false;
		}

		protected override bool IsAbleJump() {
			return false;
		}
	}
}