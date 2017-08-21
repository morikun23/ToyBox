using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	[System.Serializable]
	public class Arm : ObjectBase {

		public enum Phase {
			Lengthen,
			Freeze,
			Shorten
		}

		//---------------------------
		// UnityComponent
		//---------------------------

		//---------------------------
		// メンバー
		//---------------------------

		//ハンド
		public Hand m_hand { get; private set; }

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
		public Stack<IArmCommand> m_task { get; private set; }

		//現在のフェーズ
		public Phase m_currentPhase { get; private set; }

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_player"></param>
		public void Initialize(Player arg_player) {

			m_task = new Stack<IArmCommand>();

			m_hand = GetComponentInChildren<Hand>();

			m_hand.Initialize(this);
			m_currentRange = 5f;
			m_currentLength = 0f;
			m_currentPhase = Phase.Freeze;

		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void UpdateByFrame(Player arg_player) {

			//プレイヤーの座標に依存する
			m_transform.position = arg_player.transform.position;

			switch (m_currentPhase) {
				case Phase.Lengthen:
				if (IsLengthened()) {
					//TODO:Handが所持しているものによって処理を変更する
					m_currentPhase = Phase.Shorten;
					break;
				}

				m_task.Push(new ArmLengthenCommand(this , 2.0f));
				m_task.Peek().Execute(this);

				break;

				case Phase.Shorten:
				if (IsShotened()) {
					m_currentPhase = Phase.Freeze;
					break;
				}
				m_task.Pop().Undo(this);
				break;

				case Phase.Freeze:
				break;
			}
			
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
			return m_transform.position;
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