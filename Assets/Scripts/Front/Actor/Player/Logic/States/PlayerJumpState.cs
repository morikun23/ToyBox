//担当：森田　勝
//概要：プレイヤーのジャンプ状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerJumpState : IPlayerState {

		private const float JUMP_POWER = 250f;

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnEnter(PlayerComponent arg_player) {
			arg_player.m_rigidbody.AddForce(Vector2.up * JUMP_POWER);

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
			
		}

		public virtual IPlayerState GetNextState(PlayerComponent arg_player) {
			return new PlayerIdleState();
		}
	}
}