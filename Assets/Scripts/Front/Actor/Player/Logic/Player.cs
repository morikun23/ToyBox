using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class Player : PlayerComponent {

		private Arm m_arm;

		public override ArmComponent Arm {
			get {
				return m_arm;
			}
		}

		private Hand m_hand;

		public override HandComponent Hand {
			get {
				return m_hand;
			}
		}
#if DEVELOP
		[SerializeField]
		string dev_state;
		[SerializeField]
		string dev_ground;
#endif

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			m_inputHandle = new InputHandle();
			m_currentState = new PlayerIdleState();
			m_groundedState = new OnPlayerGroundedState();
			m_currentState.OnEnter(this);
			m_groundedState.OnEnter(this);

			m_rigidbody = GetComponent<Rigidbody2D>();
			m_collider = GetComponent<BoxCollider2D>();

			m_viewer = GetComponentInChildren<PlayerViewer>();
			m_viewer.Initialize(this);
			m_arm = GetComponentInChildren<Arm>();
			m_arm.SetOwner(this);
			m_arm.Initialize();
			m_hand = FindObjectOfType<Hand>();
			m_playableArm = m_arm;
			m_playableHand = m_hand;
			m_hand.SetOwner(this);
			m_hand.Initialize();

			m_ableJump = true;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame() {

			m_isGrounded = IsGrounded();

			IPlayerState nextState = m_currentState.GetNextState(this);
			if (nextState != null) {
				m_currentState.OnExit(this);
				m_currentState = nextState;
				m_currentState.OnEnter(this);
			}

			IPlayerState nextGround = m_groundedState.GetNextState(this);
			if (nextGround != null) {
				m_groundedState.OnExit(this);
				m_groundedState = nextGround;
				m_groundedState.OnEnter(this);
			}

			StateTransitionIfNeed(m_currentState);

#if DEVELOP
			dev_state = m_currentState.ToString();
			dev_ground = m_groundedState.ToString();
#endif
			m_groundedState.OnUpdate(this);
			m_currentState.OnUpdate(this);
			m_arm.UpdateByFrame();
			m_hand.UpdateByFrame();

			m_viewer.FlipByDirection(m_direction);
		}

		/// <summary>
		/// 入力を受け取れるかのチェック
		/// </summary>
		/// <returns></returns>
		public override bool CallWhenWishItem() {

			if (Hand.m_itemBuffer) {
				return false;
			}

			if (m_currentState.GetType() == typeof(PlayerReachState)) {
				return false;
			}

			
			return true;
		}

		private void StateTransitionIfNeed(IPlayerState arg_currentState) {
			
		}
	}
}