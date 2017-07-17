//担当：森田　勝
//概要：ゲーム内のプレイアブルキャラクターの描画クラス
//　　　マジックハンドの制御は自身がトリガーとなる
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class Player : CharacterBase {
		
		//自身のLogic部分、検証用にInspectorで制御できるようになっている
		private Logic.Player m_logic;
		
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_logic">ロジック部</param>
		public void Initialize(Logic.Player arg_logic) {
			base.Initialize();
			m_logic = arg_logic;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_logic">ロジック部</param>
		public void UpdateByFrame(Logic.Player arg_logic) {
			m_logic = arg_logic;
			
			//Logicで調整された座標に描画させる
			m_transform.position = m_logic.m_position;
			m_transform.eulerAngles = new Vector3(0 , 0 , arg_logic.m_rotation);
			m_spriteRenderer.sortingOrder = m_depth;

			//向きに応じてスプライトの反転を行う
			switch (m_logic.m_direction) {
				case Logic.CharacterBase.Direction.LEFT:
				m_spriteRenderer.flipX = true; break;
				case Logic.CharacterBase.Direction.RIGHT:
				m_spriteRenderer.flipX = false; break;
			}
		}
	}
}