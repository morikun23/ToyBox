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


		public void Initialize() {
			m_inputHandle = new InputHandle();
			m_currentState = new OnPlayerGroundedState();
			m_currentState.OnEnter(this);
			m_rigidbody = GetComponent<Rigidbody2D>();
			m_collider = GetComponent<BoxCollider2D>();

			m_viewer = GetComponentInChildren<PlayerViewer>();
			m_viewer.Initialize(this);
			m_arm = GetComponentInChildren<Arm>();
			m_arm.SetOwner(this);
			m_arm.Initialize();
			m_hand = FindObjectOfType<Hand>();
			m_playableHand = m_hand;
			m_hand.SetOwner(this);
			m_hand.Initialize();
		}

		public void UpdateByFrame() {

			m_isGrounded = IsGrounded();

			IPlayerState nextState = m_currentState.GetNextState(this);

			if (nextState != null) {
				StateTransition(nextState);
			}
			
			m_currentState.OnUpdate(this);
			m_arm.UpdateByFrame();
			m_hand.UpdateByFrame();

			m_viewer.FlipByDirection(m_direction);
		}
	}
}