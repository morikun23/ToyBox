using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerReachState : IPlayerState {

		IPlayerCommand m_commandBuf;

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnEnter(Player arg_player) {
			m_commandBuf = arg_player.m_task.Peek();
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnUpdate(Player arg_player) {
			if (arg_player.m_task.Count > 0) {
				arg_player.m_task.Dequeue().Execute(arg_player);
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
		public void OnExit(Player arg_player) {
			arg_player.m_task.Clear();
		}


		public void AddTask(Player arg_player , IPlayerCommand arg_command) {
			
		}
	}
}