using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerViewer : AnimationObject {

		//横のスケールのバッファ
		private float m_xScaleBuf;

		public void Initialize(Player arg_player) {
			m_xScaleBuf = m_transform.localScale.x;
		}

		public void FlipByDirection(ActorBase.Direction arg_direction) {
			bool flip = arg_direction == ActorBase.Direction.RIGHT;
			m_spriteRenderer.flipX = flip;
		}
	}
}