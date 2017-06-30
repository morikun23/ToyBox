using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class Player : ActorBuddy {
		
		private const string m_spritePass = "Actor/BD_Boy";

		[SerializeField]
		private Logic.Player m_logic;

		public void Initialize(Logic.Player arg_player) {
			base.Initialize();
		}
		
		public void UpdateByFrame(Logic.Player arg_player) {
			m_logic = arg_player;
			m_sprite = Resources.Load<Sprite>(m_spritePass);
			m_spriteRenderer.sprite = m_sprite;
			
			transform.position = m_logic.m_position;

			float xScale = m_logic.m_width / (m_sprite.bounds.size.x * DEFAULT_SIZE);
			float yScale = m_logic.m_height / (m_sprite.bounds.size.y * DEFAULT_SIZE);
			transform.localScale = new Vector2(xScale , yScale);
			switch (m_logic.m_direction) {
				case Logic.ActorBuddy.Direction.LEFT:
				m_spriteRenderer.flipX = true; break;
				case Logic.ActorBuddy.Direction.RIGHT:
				m_spriteRenderer.flipX = false; break;
			}

		}
	}
}