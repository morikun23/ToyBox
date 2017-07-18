//担当：森田　勝
//概要：プレイヤーの腕を制御するクラス
//　　　伸び縮みしたり、当たり判定があったり
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	[System.Serializable]
	public class Arm : ActorBase {
		
		//最大の射程
		private const float MAX_RANGE = 10f;
	
		//現在の射程
		public float m_currentRange { get; private set; }

		//ターゲット座標（タップされた座標）
		public Vector2 m_targetPosition { get; private set; }

		public float m_currentAngle { get; set; }

		//現在の射出距離
		public float m_currentLength;

		//アームの状態
		public IArmState m_currentState { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// 必要なメモリを取得
		/// </summary>
		public Arm() { }

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_controller">コントローラー部</param>
		public void Initialize(Controller.Arm arg_controller) {
			//プレイヤーの座標に依存する
			m_position = arg_controller.transform.position;
			m_rotation = arg_controller.transform.eulerAngles.z;

			m_currentRange = 5f;
			m_currentLength = 0f;
			m_currentState = new ArmIdleState();
			m_currentState.OnEnter(this);
		}
		 
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_controller">コントローラー部</param>
		public void UpdateByFrame(Controller.Arm arg_controller) {

			//プレイヤーの座標に依存する
			m_position = arg_controller.transform.position;
			
			//ステートの更新
			m_currentState.OnUpdate(this);

		}

		/// <summary>
		/// タップされた座標をアームに伝える
		/// </summary>
		/// <param name="arg_position"></param>
		public void SetTargetPosition(Vector2 arg_position) {
			m_targetPosition = arg_position;
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
			return m_position;
		}

		/// <summary>
		/// ステートを変更する
		/// </summary>
		/// <param name="arg_nextState"></param>
		public void StateTransition(IArmState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}

		/// <summary>
		/// アームが動作しているかを調べる
		/// </summary>
		/// <returns></returns>
		public bool IsActive() {
			return m_currentState.GetType() != typeof(ArmIdleState);
		}

		
		/// <summary>
		/// すでに伸びきっているかどうか
		/// </summary>
		/// <returns></returns>
		public bool IsLengthened() {
			return m_currentLength >= (m_targetPosition - GetBottomPosition()).magnitude;
		}

		/// <summary>
		/// すでに縮みきっているかどうか
		/// </summary>
		/// <returns></returns>
		public bool IsShotened() {
			return m_currentLength <= 0;
		}
	}
}