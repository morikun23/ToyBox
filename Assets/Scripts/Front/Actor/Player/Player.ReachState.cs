using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public partial class Player {
		public class ReachState : IPlayerState {

			/// <summary>
			/// プレイヤー自身への参照
			/// </summary>
			private Player m_player;

			/// <summary>状態遷移時の加速度のバッファ</summary>
			private Vector2 m_velocityBuf;

			public ReachState(Player arg_player) {
				m_player = arg_player;
			}

			void IPlayerState.OnEnter() {
				m_player.m_rigidbody.isKinematic = true;
				m_velocityBuf = m_player.m_rigidbody.velocity;
				m_player.m_rigidbody.velocity = Vector2.zero;
			}

			void IPlayerState.OnUpdate() {

			}

			void IPlayerState.OnExit() {

				m_player.m_rigidbody.isKinematic = false;
				m_player.m_rigidbody.velocity = m_velocityBuf;

				if (m_player.m_jump) {
					m_player.Jump(m_player.m_jumpDirection);
					m_player.m_jump = false;
				}

			}
			
			IPlayerState IPlayerState.GetNextState() {
				if (m_player.m_dead) return new DeadState(m_player);
				if (!m_player.m_reach) return new IdleState(m_player);
				return null;
			}

		}
	}
}
