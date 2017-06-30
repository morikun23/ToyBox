using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class ActorBase : MonoBehaviour {

		public const int DEFAULT_SIZE = 60;

		protected Sprite m_sprite;
		protected SpriteRenderer m_spriteRenderer;
		protected Collider2D m_collider;
		protected Transform m_transform;
	}
}