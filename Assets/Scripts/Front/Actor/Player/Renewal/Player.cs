using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {
	public partial class Player : MonoBehaviour ,IArmCallBackReceiver {

		public enum Direction {
			LEFT = -1,
			RIGHT = 1
		}

		[SerializeField]
		private Direction m_currentDirection;

		public interface IPlayerState {

			void OnEnter();

			void OnUpdate();

			void OnExit();

			IPlayerState GetNextState();
		}

		private IPlayerState m_currentState;

		[SerializeField]
		private Rigidbody2D m_rigidbody;

		[SerializeField]
		private Animator m_animator;

		private bool m_isGrounded;

		[SerializeField]
		private float m_runSpeed;

		[SerializeField]
		private float m_jumpPower;
		
		[SerializeField]
		private bool m_leftRun;
		[SerializeField]
		private bool m_rightRun;

		private bool m_jump;

		private bool m_reach;

		[SerializeField]
		private Arm m_arm;

		[SerializeField]
		private Hand m_hand;

		public Arm PlayableArm {
			get {
				return m_arm;
			}
		}

		public Hand PlayableHand {
			get {
				return m_hand;
			}
		}

		[SerializeField]
		private SpriteRenderer m_spriteRenderer;

		// Use this for initialization
		void Start() {
			m_currentState = new IdleState(this);
			m_arm.Initialize(this);
			m_hand.Initialize(m_arm);
		}

		// Update is called once per frame
		void Update() {
			
			IPlayerState nextState = m_currentState.GetNextState();
			if (nextState != null) {
				StateTransition(nextState);
			}
			m_currentState.OnUpdate();

			//向きをスプライトに反映
			m_spriteRenderer.flipX = m_currentDirection == Direction.RIGHT;
		}
		
		public System.Type GetCurrentState() {
			return m_currentState.GetType();
		}

		public void Run(Direction arg_direction) {
			if(GetCurrentState() == typeof(RunState)) {
				m_currentDirection = arg_direction;
			}
			if (arg_direction == Direction.LEFT) m_leftRun = true;
			else if (arg_direction == Direction.RIGHT) m_rightRun = true;
		}

		public void Stop(Direction arg_direction) {
			if (arg_direction == Direction.LEFT) m_leftRun = false;
			else if (arg_direction == Direction.RIGHT) m_rightRun = false;
		}

		public void Jump() {
			if (m_isGrounded) {
				m_rigidbody.AddForce(Vector2.up * m_jumpPower);
				
			}
		}

		public void Dead() {
			
		}

		public void Revive() {
			
		}
		
		/// <summary>
		/// ステートの遷移を行う
		/// </summary>
		/// <param name="arg_nextState">次のステート</param>
		private void StateTransition(IPlayerState arg_nextState) {
			if (m_currentState != null) {
				m_currentState.OnExit();
				m_currentState = null;
			}
			if (arg_nextState != null) {
				m_currentState = arg_nextState;
				m_currentState.OnEnter();
			}
		}
		
		/// <summary>
		/// 着地時の処理
		/// 地面と接触したときにコールバックとして実行される
		/// </summary>
		private void OnGroundEnter() {
			m_animator.SetBool("OnGround" , true);
			m_animator.SetBool("OnAir" , false);
			m_isGrounded = true;
		}

		/// <summary>
		/// 離陸時の処理
		/// 地面と接触がなくなったときにコールバックとして実行される
		/// </summary>
		private void OnGroundExit() {
			m_animator.SetBool("OnGround" , false);
			m_animator.SetBool("OnAir" , true);
			m_isGrounded = false;
		}

		void IArmCallBackReceiver.OnStartLengthen() {
			m_reach = true;
		}

		void IArmCallBackReceiver.OnEndShorten() {
			m_reach = false;
		}
	}
}