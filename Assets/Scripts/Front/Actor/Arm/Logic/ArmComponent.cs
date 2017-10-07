//担当：森田　勝
//概要：アームとしてのふるまいを持ったクラス
//　　　実際にはFSMの形となっている。
//　　　また、このクラスを動かす場合は処理をループさせるクラスを作成してください。
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class ArmComponent : PlayableArm {

		public bool m_isActive;

		public bool m_shorten;

		//---------------------------
		// UnityComponent
		//---------------------------

		//---------------------------
		// メンバー
		//---------------------------

		public PlayerComponent m_owner { get; protected set; }

		public IArmState m_currentState { get; protected set; }

		//現在の回転
		public float m_currentAngle { get; set; }

		//現在の射出距離
		public float m_currentLength;

		//アームの長さのバッファ
		public Stack<float> m_lengthBuf { get; protected set; }

		protected ArmViewer m_viewer;

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

		protected void StateTransition(IArmState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}

		public void StartLengthen() {
			m_isActive = true;
		}

		public void SetOwner(PlayerComponent arg_player) {
			m_owner = arg_player;
		}
	}
}