using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class Arm : MonoBehaviour {

		public LineRenderer m_lineRenderer;

		private Rigidbody2D m_rigidbody;
		private BoxCollider2D m_collider;

		private const string m_spritePass = "Actor/BD_Arm";

		public void Initialize(Logic.Arm arg_arm) {
			m_lineRenderer = gameObject.AddComponent<LineRenderer>();
			m_lineRenderer.material.color = Color.black;
			
			m_lineRenderer.SetWidth(0.1f , 0.1f);
		}

		public void UpdateByFrame(Logic.Arm arg_arm) {
			transform.position = arg_arm.m_position;
			transform.localScale = arg_arm.m_scale;

			if (arg_arm.IsActive()) {
				m_lineRenderer.numPositions = 2;
				m_lineRenderer.SetPosition(0 , arg_arm.GetBottomPosition());
				m_lineRenderer.SetPosition(1 , arg_arm.GetBottomPosition() + arg_arm.GetTopPosition());
			}
			else {
				m_lineRenderer.numPositions = 0;
			}
		}
	}
}