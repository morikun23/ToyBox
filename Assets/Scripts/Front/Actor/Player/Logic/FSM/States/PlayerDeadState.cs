//担当：森田　勝
//概要：プレイヤーの死亡状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerDeadState : IPlayerState {
		
		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnEnter(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.Play("Dead" , 0);
			arg_player.m_viewer.m_animator.SetBool("Dead" , true);
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnUpdate(PlayerComponent arg_player) {

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnExit(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("Dead" , false);
		}

		public virtual IPlayerState GetNextState(PlayerComponent arg_player) {
			return null;
		}

	}
}