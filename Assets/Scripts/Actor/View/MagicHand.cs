using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class MagicHand : MonoBehaviour {
		
		private SpriteRenderer m_spriteRenderer;

		private Rigidbody2D m_rigidbody;
		private BoxCollider2D m_collider;

		private const string m_spritePass = "Actor/BD_MagicHand";

		public void Initialize(Logic.MagicHand arg_magicHand) {
			m_spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			m_spriteRenderer.sprite = Resources.Load<Sprite>(m_spritePass);
		}

		public void UpdateByFrame(Logic.MagicHand arg_magicHand) {
			transform.position = arg_magicHand.m_position;
			transform.localScale = arg_magicHand.m_scale;
		}
	}
}