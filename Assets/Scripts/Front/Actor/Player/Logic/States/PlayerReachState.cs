//担当：森田　勝
//概要：プレイヤーの射出状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerReachState : IPlayerState {

		private IPlayerState m_stateBuf;

		private Vector2 m_velocityBuf;

		public PlayerReachState(IPlayerState arg_state) {
			m_stateBuf = arg_state;
		}

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnEnter(Player arg_player) {
			m_velocityBuf = arg_player.m_rigidbody.velocity;
			arg_player.m_rigidbody.isKinematic = true;
			arg_player.m_rigidbody.velocity = Vector2.zero;
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnUpdate(Player arg_player) {
			
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnExit(Player arg_player) {
			arg_player.m_rigidbody.isKinematic = false;

			if(m_velocityBuf.y > 0) { m_velocityBuf.y = 0; }

			arg_player.m_rigidbody.velocity = m_velocityBuf;
		}

		public virtual IPlayerState GetNextState(Player arg_player) {
			if (!arg_player.m_inputHandle.m_reach) {
				return m_stateBuf;
			}
			return null;
		}
	}
}