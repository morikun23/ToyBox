//担当：森田　勝
//概要：ゲーム内のオブジェクトの描画用の基底クラス
//　　　初期化は自身の初期化関数で行ってください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public abstract class ActorBase : MonoBehaviour {

		//１スプライトのデフォルトサイズ
		public const int DEFAULT_SIZE = 60;
		
		//SpriteRendererコンポーネント（スプライト描画用）
		protected SpriteRenderer m_spriteRenderer;

		//Transformコンポーネント(座標などの制御用)
		protected Transform m_transform;

		//重なり優先度
		public int m_depth;

		/// <summary>
		/// 初期化
		/// </summary>
		protected virtual void Initialize() {
			m_spriteRenderer = GetComponent<SpriteRenderer>();
			m_transform = this.transform;
		}
		
	}
}