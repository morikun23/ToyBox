using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public partial class Player {
		public class DeadState : IPlayerState {

			/// <summary>
			/// プレイヤー自身への参照
			/// </summary>
			private Player m_player;

			public DeadState(Player arg_player) {
				m_player = arg_player;
			}

			void IPlayerState.OnEnter() {
				
			}

			void IPlayerState.OnUpdate() {

			}

			void IPlayerState.OnExit() {
				m_player.gameObject.SetActive(false);
			}
			
			IPlayerState IPlayerState.GetNextState() {
				if (!m_player.m_dead) return new IdleState(m_player);
				return null;
			}

		}
	}
}