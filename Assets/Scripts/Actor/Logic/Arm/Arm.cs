using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class Arm {

		//TODO:基底クラス化
		public Vector2 m_position;
		public Vector2 m_scale;

		private const float MAX_RANGE = 10f;

		public float m_currentRange { get; private set; }

		public float m_currentAngle;

		public float m_currentLength;

		private Queue<IArmAction> m_tasks;

		private bool m_isActive;

		public void Initialize(Player arg_player) {
			m_position = arg_player.m_position + Vector2.up;
			
			m_currentAngle = 0;
			m_currentRange = 5f;
			m_currentLength = 0f;
			m_tasks = new Queue<IArmAction>();
			m_isActive = m_tasks.Count > 0;
		}

		public void UpdateByFrame(Player arg_player) {
			m_position = arg_player.m_position + Vector2.up;
			
			if (m_tasks.Count > 0) {
				m_isActive = true;
				IArmAction currentTask = m_tasks.Peek();
				currentTask.OnUpdate(this);
			}
			else {
				m_isActive = false;
			}
		}

		public bool IsActive() {
			return m_tasks.Count > 0;
		}

		public void Rotate(float arg_angle) {
			m_currentAngle += arg_angle;
			m_currentAngle = Mathf.Clamp(m_currentAngle , 0 , 180);
		}

		public Vector2 GetTopPosition() {
			Vector2 pos = new Vector2(Mathf.Cos(Mathf.Deg2Rad * m_currentAngle),Mathf.Sin(Mathf.Deg2Rad * m_currentAngle)).normalized;
			pos *= m_currentLength;
			return pos;
		}

		public Vector2 GetBottomPosition() {
			return m_position;
		}

		public void AddTask(IArmAction arg_armAction) {
			m_tasks.Enqueue(arg_armAction);
		}
		
		
	}
}