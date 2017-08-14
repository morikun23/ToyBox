using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	[System.Serializable]
	public class Player : ActorBase {

		//--------------------------
		//UnityComponent
		//--------------------------
		
		//RigidBody
		private Rigidbody2D m_rigidbody { get; set; }
		
		//Collider
		private BoxCollider2D m_collider { get; set; }

		//--------------------------
		//メンバ
		//--------------------------

		//アーム
		public Arm m_arm { get; private set; }

		//自身の状態（Stateパターンでの実装）
		public IPlayerState m_currentState { get; private set; }
		
		//タスク
		public Queue<IPlayerCommand> m_task { get; private set; }

		//地面に接しているか
		public bool m_isGrounded { get; private set; }

		public void Initialize() {
			m_task = new Queue<IPlayerCommand>();
			m_rigidbody = GetComponent<Rigidbody2D>();
			m_collider = GetComponent<BoxCollider2D>();
			m_arm = GetComponentInChildren<Arm>();
			m_arm.Initialize(this);
		}

		public void UpdateByFrame() {
			m_arm.UpdateByFrame(this);
			
		}

		public void View() {
			m_spriteRenderer.sortingOrder = m_depth;

			//向きに応じてスプライトの反転を行う
			switch (m_direction) {
				case Direction.LEFT:
				m_spriteRenderer.flipX = true; break;
				case Direction.RIGHT:
				m_spriteRenderer.flipX = false; break;
			}
		}

		/// <summary>
		/// タスクを追加する
		/// </summary>
		/// <param name="arg_command"></param>
		public void AddTask(IPlayerCommand arg_command) {
			m_currentState.AddTaskIfAble(this , arg_command);
		}

		/// <summary>
		/// 地面に着いているかを調べる
		/// </summary>
		/// <returns>着地しているか</returns>
		public bool IsGrounded() {
			return Physics2D.BoxCast(transform.position , m_collider.bounds.size , 0f , Vector2.down , 0.05f , 1 << LayerMask.NameToLayer("Ground"));
		}

		/// <summary>
		/// ステートを遷移させる
		/// </summary>
		/// <param name="arg_nextState">次の状態</param>
		public void StateTransition(IPlayerState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}
	}
}