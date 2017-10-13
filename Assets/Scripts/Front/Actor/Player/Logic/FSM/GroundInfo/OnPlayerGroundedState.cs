using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class OnPlayerGroundedState : IPlayerGroundInfo {

		//ジャンプ可能になるまでの必要時間
		private const float m_jumpInterval = 0.1f;

		private float m_elapsedTime;

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnEnter(PlayerComponent arg_player) {
			m_elapsedTime = 0;
			arg_player.m_viewer.m_animator.SetBool("OnGround" , true);
			arg_player.m_ableJump = true;
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnUpdate(PlayerComponent arg_player) {
			m_elapsedTime += Time.deltaTime;
			if(m_elapsedTime > m_jumpInterval) {
				m_elapsedTime = 0;
				
			}
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnExit(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("OnGround" , false);
		}

		public virtual IPlayerGroundInfo GetNextState(PlayerComponent arg_player) {
			if (!arg_player.m_isGrounded) { return new OnPlayerAirState(); }
			return null;
		}
	}
}