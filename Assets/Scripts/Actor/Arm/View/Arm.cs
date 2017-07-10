//担当：森田　勝
//概要：アームの描画クラス
//　　　実際の描画はLineRendererを使用する
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class Arm : ActorBase {

		[SerializeField]
		Logic.Arm m_logic;

		//LineRendererコンポーネント（直線の描画用）
		private LineRenderer m_lineRenderer { get; set; }
		
		/// <summary>
		/// 初期化
		/// 各コンポーネントも取得する
		/// </summary>
		/// <param name="arg_arm">ロジック部</param>
		public void Initialize(Logic.Arm arg_logic) {
			m_logic = arg_logic;
			m_spriteRenderer = GetComponent<SpriteRenderer>();
			m_transform = this.transform;
			m_lineRenderer = GetComponent<LineRenderer>();
			m_lineRenderer.material.color = Color.black;
			m_lineRenderer.startWidth = 0.1f;
			m_lineRenderer.endWidth = 0.1f;
		}

		public void UpdateByFrame(Logic.Arm arg_logic) {
			m_logic = arg_logic;

			m_transform.position = m_logic.m_position;
			m_transform.eulerAngles = new Vector3(0 , 0 , arg_logic.m_rotation);
			m_lineRenderer.sortingOrder = m_depth;

			if (m_logic.m_isActive) {
				m_lineRenderer.numPositions = 2;
				m_lineRenderer.SetPosition(0 , m_logic.GetBottomPosition());
				m_lineRenderer.SetPosition(1 , m_logic.GetTopPosition());
			}
			else {
				m_lineRenderer.numPositions = 0;
			}
		}
	}
}