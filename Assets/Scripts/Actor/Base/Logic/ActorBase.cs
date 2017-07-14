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
	
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="arg_isActive">デフォルトでアクティブ状態を設定できます</param>
		protected ActorBase(){ }

	}
}