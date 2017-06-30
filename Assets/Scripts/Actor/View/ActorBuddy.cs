using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class ActorBuddy : ActorBase {

		protected Rigidbody2D m_rigidbody;

		public virtual void Initialize() {
			m_spriteRenderer = GetComponent<SpriteRenderer>();
			m_rigidbody = GetComponent<Rigidbody2D>();
			m_transform = this.transform;
		}

		public virtual void UpdateByFrame() {

		}

	}
}  