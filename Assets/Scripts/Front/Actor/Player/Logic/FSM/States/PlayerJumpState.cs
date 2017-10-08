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
			arg_player.m_viewer.m_animator.SetBool("Jump" , true);
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnUpdate(PlayerComponent arg_player) {

			ResetGravity(arg_player.m_rigidbody);

			arg_player.m_rigidbody.AddForce(Vector2.up * JUMP_POWER);
			//無限ジャンプを防ぐためのフラグを変更
			arg_player.m_ableJump = false;
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnExit(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("Jump" , false);
		}

		public virtual IPlayerState GetNextState(PlayerComponent arg_player) {
			return new PlayerIdleState();
		}

		private void ResetGravity(Rigidbody2D arg_rigidbody) {
			//気持ちよくジャンプさせるため重力加速をリセットさせる
			Vector2 velocity = arg_rigidbody.velocity;
			velocity.y = 0;
			arg_rigidbody.velocity = velocity;
		}
	}
}