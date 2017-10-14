using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class OnPlayerAirState : IPlayerGroundInfo {

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnEnter(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("OnAir" , true);
			arg_player.m_ableJump = false;
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
			arg_player.m_viewer.m_animator.SetBool("OnAir" , false);
		}

		public virtual IPlayerGroundInfo GetNextState(PlayerComponent arg_player) {
			if (arg_player.m_isGrounded) { return new OnPlayerGroundedState(); }
			return null;
		}
	}
}