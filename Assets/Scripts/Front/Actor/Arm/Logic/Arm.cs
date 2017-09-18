using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	[System.Serializable]
	public class Arm : ObjectBase {

		public bool lengthen;

		//---------------------------
		// UnityComponent
		//---------------------------

		//---------------------------
		// メンバー
		//---------------------------

		public Hand m_hand { get; private set; }
		
		public IArmState m_currentState { get; private set; }

		//ターゲット座標（タップされた座標）
		public Vector2 m_targetPosition { get; private set; }

		public float m_targetLength { get; private set; }

		public float m_currentAngle { get; set; }

		//現在の射出距離
		public float m_currentLength;

		//アームの長さのバッファ
		public Stack<float> m_lengthBuf { get; private set; }

		ArmViewer m_viewer;

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_player"></param>
		public void Initialize(Player arg_player) {

			m_hand = FindObjectOfType<Hand>();
			m_hand.Initialize(this);

			m_currentState = new ArmStandByState();
			m_lengthBuf = new Stack<float>();
			m_currentLength = 0f;
			
			m_viewer = GetComponentInChildren<ArmViewer>();
			m_viewer.Initialize(this);
			
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void UpdateByFrame(Player arg_player) {

			//プレイヤーの座標に依存する
			m_transform.position = arg_player.transform.position;

			IArmState nextState = m_currentState.GetNextState(this);
			if(nextState != null) {
				StateTransition(nextState);
			}
			m_currentState.OnUpdate(this);
			m_hand.UpdateByFrame(this);

			m_viewer.UpdateByFrame(this);
		}
		
		/// <summary>
		/// タップされた座標をアームに伝える
		/// </summary>
		/// <param name="arg_position"></param>
		public void SetTargetPosition(Vector2 arg_position) {
			m_targetPosition = arg_position;
			m_targetLength = (m_targetPosition - (Vector2)m_transform.position).magnitude;
		}

		/// <summary>
		/// アームの先端の座標を取得する
		/// </summary>
		/// <returns>※絶対座標</returns>
		public Vector2 GetTopPosition() {
			Vector2 pos = m_targetPosition - GetBottomPosition();
			pos = pos.normalized * m_currentLength;
			return this.GetBottomPosition() + pos;
		}

		/// <summary>
		/// アームの根元の座標を取得する
		/// </summary>
		/// <returns>※絶対座標</returns>
		public Vector2 GetBottomPosition() {
			return m_transform.position;
		}

		private void StateTransition(IArmState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}
	}
}