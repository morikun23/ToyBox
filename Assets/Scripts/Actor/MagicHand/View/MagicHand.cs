//担当：森田　勝
//概要：マジックハンドの描画を担当するクラス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class MagicHand : ActorBase {

		//自身のLogic部分、検証用にInspectorで制御できるようになっている
		[SerializeField]
		private Logic.MagicHand m_logic;

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_logic">ロジック部</param>
		public void Initialize(Logic.MagicHand arg_logic) {
			base.Initialize();
			m_logic = arg_logic;
			m_depth = -1;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_logic">ロジック部</param>
		public void UpdateByFrame(Logic.MagicHand arg_logic) {
			m_transform.position = m_logic.m_position;
			m_transform.eulerAngles = Vector3.forward * arg_logic.m_rotation;
			m_spriteRenderer.sortingOrder = m_depth;

		}
	}
}