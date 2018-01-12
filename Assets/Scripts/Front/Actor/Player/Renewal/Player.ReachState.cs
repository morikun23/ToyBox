using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {
	public partial class Player {
		private class ReachState : IPlayerState {

			/// <summary>
			/// プレイヤー自身への参照
			/// </summary>
			private Player m_player;

			public ReachState(Player arg_player) {
				m_player = arg_player;
			}

			void IPlayerState.OnEnter() {
				m_player.m_animator.SetBool("Reach" , true);

				m_player.m_rigidbody.isKinematic = true;
				m_player.m_rigidbody.velocity = Vector2.zero;
				m_player.m_hand.gameObject.SetActive(true);
			}

			void IPlayerState.OnUpdate() {

			}

			void IPlayerState.OnExit() {
				m_player.m_animator.SetBool("Reach" , false);
				m_player.m_rigidbody.isKinematic = false;
				m_player.m_rigidbody.velocity = Vector2.zero;
				m_player.m_hand.gameObject.SetActive(false);
			}
			
			IPlayerState IPlayerState.GetNextState() {
				if (!m_player.m_reach) return new IdleState(m_player);
				return null;
			}

		}
	}
}
