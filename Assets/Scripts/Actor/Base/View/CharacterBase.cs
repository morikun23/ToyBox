//担当：森田　勝
//概要：ゲーム内のキャラクターの描画用の基底クラス
//　　　各キャラクターはアニメーションを行うものとします。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public abstract class CharacterBase : ActorBase {

		//アニメーターコンポーネント
		Animator m_animator;

		/// <summary>
		/// 初期化
		/// </summary>
		protected override void Initialize() {
			base.Initialize();
			m_animator = GetComponent<Animator>();
		}

		/// <summary>
		/// 更新
		/// </summary>
		public virtual void UpdateByFrame() {

		}

	}
}  