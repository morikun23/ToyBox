//担当：森田　勝
//概要：プレイヤーのステートを実装するにあたって
//　　　共通部分をここにまとめた。
//　　　ステートの遷移ができるかなどの処理はここでおこなう
//　　　実際にステートを実装するときは、遷移できないステートだけ
//　　　関数をオーバーライドしてfalseを返してください
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class PlayerStateBase : IPlayerState {

		protected virtual bool AbleRun{ get { return true; } }

		protected virtual bool AbleJump{ get { return true; } }

		protected virtual bool AbleReach{ get { return true; } }


		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public abstract void OnEnter(Player arg_player);

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public abstract void OnUpdate(Player arg_player);

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public abstract void OnExit(Player arg_player);

		/// <summary>
		/// 指定されたタスクが追加可能であれば
		/// 追加をし、対応したステートに遷移させる
		/// </summary>
		/// <param name="arg_player"></param>
		/// <param name="arg_command"></param>
		public virtual void AddTaskIfAble(Player arg_player , IPlayerCommand arg_command) {
			if (arg_command.GetType() == typeof(PlayerRunLeftCommand)) {
				if (!AbleRun) return;
				arg_player.StateTransition(new PlayerRunState());
				arg_player.m_task.Enqueue(arg_command);
			}
			else if (arg_command.GetType() == typeof(PlayerRunRightCommand)) {
				if (!AbleRun) return;
				arg_player.StateTransition(new PlayerRunState());
				arg_player.m_task.Enqueue(arg_command);
			}
			else if (arg_command.GetType() == typeof(PlayerJumpCommand)) {
				if (!AbleJump) return;
				arg_player.StateTransition(new PlayerJumpState());
				arg_player.m_task.Enqueue(arg_command);
			}
			else if (arg_command.GetType() == typeof(PlayerReachCommand)) {
				if (!AbleReach) return;
				arg_player.StateTransition(new PlayerReachState());
				arg_player.m_task.Enqueue(arg_command);
			}
		}
	}
}