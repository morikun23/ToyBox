using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public partial class Player {
		public class RunState : IPlayerState {

			/// <summary>
			/// プレイヤー自身への参照
			/// </summary>
			private Player m_player;
			private BoxCollider2D m_collider;

			private BoxCollider2D Body {
				get {
					if(m_collider == null) {
						m_collider = m_player.GetComponent<BoxCollider2D>();
					}
					return m_collider;
				}
			}

			public RunState(Player arg_player) {
				m_player = arg_player;
			}

			void IPlayerState.OnEnter() {
				m_player.m_animator.SetBool("Run" , true);
				AudioManager.Instance.RegisterSE("Foot" , "SE_Player_Walk");
				AudioManager.Instance.PlaySE("Foot" , true);
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

				//行き止まりである
				if (IsDeadEnd()) {
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
				AudioManager.Instance.ReleaseSE("Foot");
			}

			IPlayerState IPlayerState.GetNextState() {
				if (m_player.m_dead) return new DeadState(m_player);
				if (m_player.m_reach) return new ReachState(m_player);
				if (!(m_player.m_leftRun || m_player.m_rightRun)) return new IdleState(m_player);
				return null;
			}

			/// <summary>
			/// 進行方向に壁がないか調べる
			/// </summary>
			/// <returns></returns>
			private bool IsDeadEnd() {
				if (Physics2D.BoxCast(m_player.transform.position ,
					Body.bounds.size ,
					0 , Vector2.right * (int)m_player.m_currentDirection ,
					0.1f , 1 << LayerMask.NameToLayer("Ground"))) {
					return true;
				}
				return false;
			}

		}
	}
}