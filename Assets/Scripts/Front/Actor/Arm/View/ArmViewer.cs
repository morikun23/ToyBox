using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ArmViewer : ViewObject {


		//LineRendererコンポーネント（直線の描画用）
		private LineRenderer m_lineRenderer { get; set; }

		public void Initialize(Arm arg_arm) {
			m_lineRenderer = GetComponent<LineRenderer>();
			m_lineRenderer.material.color = Color.black;
			m_lineRenderer.startWidth = 0.1f;
			m_lineRenderer.endWidth = 0.1f;
		}

		public void UpdateByFrame(Arm arg_arm) {

			//m_lineRenderer.sortingOrder = m_depth;

			if (arg_arm.m_currentState.GetType() == typeof(ArmStandByState)) {
				m_lineRenderer.positionCount = 0;
			}
			else {
				m_lineRenderer.positionCount = 2;
				m_lineRenderer.SetPosition(0 , (Vector3)arg_arm.GetBottomPosition() + (Vector3.forward * -m_depth));
				m_lineRenderer.SetPosition(1 , (Vector3)arg_arm.GetTopPosition() + (Vector3.forward * -m_depth));
			}
		}
	}
}