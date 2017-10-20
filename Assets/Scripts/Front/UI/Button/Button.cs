﻿//担当：森田　勝
//概要：UIボタンの機能を実装するための基底クラス
//　　　MobileInputの入力検知から関数を呼び出している
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class Button : ToyBox.MobileInput {

		//操作する対象
		protected Playable m_playable;

		//ボタンアニメーション用のコンポーネント
		protected Animator m_animator;

		//ボタンが押されているか（長押し中フラグ）
		private bool m_isPressing;

#if UNITY_EDITOR && DEVELOP
		[SerializeField]
		protected KeyCode m_key;
#endif

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void Initialize() {
			m_isPressing = false;
			m_animator = GetComponent<Animator>();
		}

		/// <summary>
		/// 更新
		/// </summary>
		public virtual void UpdateByFrame() {
			if (m_isPressing) { this.OnPress(); }
		}

		//@override
		public sealed override void Started() {
			base.Started();
			m_isPressing = true;
			this.OnDown();
		}

		//@override
		public sealed override void TouchEnd() {
			base.TouchEnd();
			m_isPressing = false;
			this.OnUp();
		}

		//@override
		public sealed override void SwipeEnd() {
			base.SwipeEnd();
			m_isPressing = false;
			this.OnUp();
		}

		/// <summary>
		/// ボタンが下がったとき
		/// </summary>
		public virtual void OnDown() {
			m_animator.SetBool("Press" , true);
		}

		/// <summary>
		/// ボタンが下がっているとき
		/// 長押し中
		/// </summary>
		public virtual void OnPress() {

		}

		/// <summary>
		/// ボタンが上がったとき
		/// </summary>
		public virtual void OnUp() {
			m_animator.SetBool("Press" , false);
		}
		
		/// <summary>
		/// 操作対象を設定する
		/// </summary>
		/// <param name="arg_playable"></param>
		public void SetControlTarget(Playable arg_playable) {
			m_playable = arg_playable;
		}
	}
}