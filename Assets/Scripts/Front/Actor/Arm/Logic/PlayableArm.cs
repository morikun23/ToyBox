//担当：森田　勝
//概要：入力クラスからHandを参照させるためのインターフェイス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class PlayableArm : ObjectBase {

		//ターゲット座標（タップされた座標）
		public Vector2 m_targetPosition { get; private set; }

		//ターゲット座標との距離の長さ
		public float m_targetLength { get; private set; }

		/// <summary>
		/// タップされた座標をアームに伝える
		/// この座標に向けてアームを伸ばす
		/// ※最初にこの処理をしていください
		/// </summary>
		/// <param name="arg_position"></param>
		public void SetTargetPosition(Vector2 arg_position) {
			m_targetPosition = arg_position;
			m_targetLength = (m_targetPosition - (Vector2)m_transform.position).magnitude;
		}

	}
}