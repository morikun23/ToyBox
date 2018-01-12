using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {
	public partial class Player {
		private class RunState : IPlayerState {

			/// <summary>
			/// プレイヤー自身への参照
			/// </summary>
			private Player m_player;
			
			public RunState(Player arg_player) {
				m_player = arg_player;
			}

			void IPlayerState.OnEnter() {
				m_player.m_animator.SetBool("Run" , true);
				//AudioManager.Instance.RegisterSE("Foot" , "SE_Player_Walk");
				//AudioManager.Instance.PlaySE("Foot" , true);
			}

			void IPlayerState.OnUpdate() {
				if (m_player.m_leftRun && m_player.m_rightRun) {
					m_player.m_rigidbody.velocity = new Vector3() {
						x = 0 ,
						y = m_player.m_rigidbody.velocity.y ,
						z = 0
					};
					return;
				}

				m_player.m_rigidbody.velocity = new Vector3() {
					x = m_player.m_runSpeed * (int)m_player.m_currentDirection ,
					y = m_player.m_rigidbody.velocity.y ,
					z = 0
				};
			}

			void IPlayerState.OnExit() {
				m_player.m_animator.SetBool("Run" , false);
				m_player.m_rigidbody.velocity = new Vector3() {
					x = 0 ,
					y = m_player.m_rigidbody.velocity.y ,
					z = 0
				};
				//AudioManager.Instance.ReleaseSE("Foot");
			}

			IPlayerState IPlayerState.GetNextState() {
				if (m_player.m_reach) return new ReachState(m_player);
				if (!(m_player.m_leftRun || m_player.m_rightRun)) return new IdleState(m_player);
				return null;
			}

		}
	}
}