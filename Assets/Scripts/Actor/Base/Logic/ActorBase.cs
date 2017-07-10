//担当：森田　勝
//概要：ゲーム内のオブジェクトの基底クラス
//　　　初期化は自身の初期化関数で行ってください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public abstract class ActorBase {

		//座標
		public Vector2 m_position;

		//回転
		public float m_rotation;

		//アクティブ状態（動作しているか）
		public bool m_isActive;
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="arg_isActive">デフォルトでアクティブ状態を設定できます</param>
		protected ActorBase(bool arg_isActive = true){ m_isActive = arg_isActive; }

		/// <summary>
		/// アクティブ状態を変更する
		/// 明示的なので、この関数を使用してください
		/// </summary>
		/// <param name="arg_value">アクティブ値</param>
		public void SetActive(bool arg_value) {
			m_isActive = arg_value;
		}

	}
}