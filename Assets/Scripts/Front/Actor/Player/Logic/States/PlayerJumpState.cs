//担当：森田　勝
//概要：プレイヤーのジャンプ状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerJumpState : PlayerStateBase , IPlayerState {

		public int Priority { get { return 2; } }

		private const float JUMP_POWER = 250f;

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
			arg_player.m_rigidbody.AddForce(Vector2.up * JUMP_POWER);
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnExit(Player arg_player) {
			
		}
		
	}
}