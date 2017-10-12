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

		private Rigidbody2D m_rigidbodyBuf;

		public override Rigidbody2D m_rigidbody {
			get {
				if(m_rigidbodyBuf == null) {
					m_rigidbodyBuf = GetComponent<Rigidbody2D>();
				}
				return m_rigidbodyBuf;
			}
		}

		[SerializeField]
		private BoxCollider2D m_bodyColliderBuf;

		public override BoxCollider2D m_body {
			get {
				//一応Find関数を使用しているが、なるべく使用したくない
				return m_bodyColliderBuf ?? transform.FindChild("Body").GetComponent<BoxCollider2D>();
			}
		}

		[SerializeField]
		private BoxCollider2D m_footBuf;

		public override BoxCollider2D m_foot {
			get {
				//一応Find関数を使用しているが、なるべく使用したくない
				return m_bodyColliderBuf ?? transform.FindChild("Foot").GetComponent<BoxCollider2D>();
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
			m_viewer = GetComponentInChildren<PlayerViewer>();
			m_viewer.Initialize(this);

			SetUpFSM();
			
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

			//地形FSM更新
			UpdateByGround();

			//ギミックFSM更新
			UpdateByGimmick();

			//行動FSM更新
			UpdateByState();

			m_arm.UpdateByFrame();
			m_hand.UpdateByFrame();

#if DEVELOP
			dev_state = m_currentState.ToString();
			dev_ground = m_currentGroundInfo.ToString();
#endif
			
			m_viewer.FlipByDirection(m_direction);
		}

		/// <summary>
		/// 入力を受け取れるかのチェック
		/// </summary>
		/// <returns></returns>
		public override bool CallWhenWishItem() {

			if (m_arm.m_isActive) {
				return false;
			}

			if (Hand.m_itemBuffer) {
				//return false;
			}

			if (m_currentState.GetType() == typeof(PlayerReachState)) {
				return false;
			}

			
			return true;
		}

		/// <summary>
		/// FSM系の初期化を行う
		/// </summary>
		private void SetUpFSM() {
			m_currentState = new PlayerIdleState();
			m_currentState.OnEnter(this);
			m_currentGroundInfo = new OnPlayerGroundedState();
			m_currentGroundInfo.OnEnter(this);
			m_currentGimmickInfo = new PlayerFreeState();
			m_currentGimmickInfo.OnEnter(this);
		}

		/// <summary>
		/// 地形による状態処理を更新
		/// </summary>
		private void UpdateByGround() {
			IPlayerGroundInfo nextGround = m_currentGroundInfo.GetNextState(this);
			if (nextGround != null) {
				GroundInfoTransition(nextGround);
			}
			m_currentGroundInfo.OnUpdate(this);
		}

		/// <summary>
		/// 基本状態を更新
		/// </summary>
		private void UpdateByState() {
			IPlayerState nextState = m_currentState.GetNextState(this);
			if (nextState != null) {
				m_currentState.OnExit(this);
				m_currentState = nextState;
				m_currentState.OnEnter(this);
			}
			m_currentState.OnUpdate(this);
		}

		/// <summary>
		/// ギミックによる状態処理を更新
		/// </summary>
		private void UpdateByGimmick() {
			IPlayerGimmickInfo nextInfo = m_currentGimmickInfo.GetNextState(this);
			if (nextInfo != null) {
				GimmickInfoTransition(nextInfo);
			}
			m_currentGroundInfo.OnUpdate(this);
		}
	}
}