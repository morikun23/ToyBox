﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerRunState : IPlayerState {

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnEnter(Player arg_player) {

		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnUpdate(Player arg_player) {
			if (arg_player.m_task.Count > 0) {
				arg_player.m_task.Dequeue().Execute(arg_player);
			}
			else {
				arg_player.StateTransition(new PlayerIdleState());
			}
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnExit(Player arg_player) {
			
		}

		public void AddTask(Player arg_player , IPlayerCommand arg_command) {
			if (arg_command.GetType() == typeof(PlayerRunLeftCommand)) {
				arg_player.m_task.Enqueue(arg_command);
			}
			else if (arg_command.GetType() == typeof(PlayerRunRightCommand)) {
				arg_player.m_task.Enqueue(arg_command);
			}
			else if (arg_command.GetType() == typeof(PlayerJumpCommand)) {
				arg_player.m_task.Enqueue(arg_command);
				arg_player.StateTransition(new PlayerJumpState());
			}
			else if (arg_command.GetType() == typeof(PlayerReachCommand)) {
				arg_player.m_task.Enqueue(arg_command);
				arg_player.StateTransition(new PlayerReachState());
			}
		}
	}
}