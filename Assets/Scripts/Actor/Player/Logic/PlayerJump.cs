using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerJump : IPlayerCommand {

		private bool m_isFinished;

		private Vector2 m_velocity;
		private float m_prev;
		private float m_t;

		public void OnEnter(Player arg_player) {
			m_velocity = arg_player.m_position;
			m_prev = m_velocity.y;
			m_t = 0.5f;
		}

		public void OnUpdate(Player arg_player) {
			if (m_t > 0) {
				m_velocity = arg_player.m_position;

				float temp = m_velocity.y;

				m_velocity.y += (m_velocity.y - m_prev);
				m_prev = temp;
				arg_player.m_position = m_velocity;
				m_t += -0.098f;
			}
		}

		public bool IsFinished() {
			return m_isFinished;
		}
	}
}