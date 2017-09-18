using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	[System.Serializable]
	public class Player : ActorBase {

		public class InputHandle {
			public bool m_run;
			public bool m_jump;
			public bool m_reach;
		}

		public InputHandle m_inputHandle;

		//--------------------------
		//UnityComponent
		//--------------------------
		
		//RigidBody
		public Rigidbody2D m_rigidbody { get; private set; }
		
		//Collider
		public BoxCollider2D m_collider { get; private set; }

		//--------------------------
		//パラメータ
		//--------------------------

		//自身の状態（Stateパターンでの実装）
		public IPlayerState m_currentState { get; private set; }
		
		//地面に接しているか
		public bool m_isGrounded { get; private set; }

		private PlayerViewer m_viewer { get; set; }

		public void Initialize() {
			m_inputHandle = new InputHandle();
			m_currentState = new OnPlayerGroundedState();
			m_currentState.OnEnter(this);
			m_rigidbody = GetComponent<Rigidbody2D>();
			m_collider = GetComponent<BoxCollider2D>();

			m_viewer = GetComponentInChildren<PlayerViewer>();
			m_viewer.Initialize(this);
		}

		public void UpdateByFrame() {

			m_isGrounded = IsGrounded();
			
			IPlayerState nextState = m_currentState.GetNextState(this);

			if (nextState != null) {
				StateTransition(nextState);
			}
			Debug.Log(m_currentState);
			
			m_currentState.OnUpdate(this);
			m_viewer.FlipByDirection(m_direction);

		}
		
		/// <summary>
		/// 地面に着いているかを調べる
		/// </summary>
		/// <returns>着地しているか</returns>
		public bool IsGrounded() {
			return Physics2D.BoxCast(transform.position , m_collider.bounds.size , 0f , Vector2.down , 0.1f , 1 << LayerMask.NameToLayer("Ground"));
		}

		/// <summary>
		/// ステートを遷移させる
		/// </summary>
		/// <param name="arg_nextState">次の状態</param>
		private void StateTransition(IPlayerState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}
	}
}