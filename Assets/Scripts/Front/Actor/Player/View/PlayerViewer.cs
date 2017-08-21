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

		public void UpdateByFrame(Player arg_player) {
			switch (arg_player.m_direction) {
				case ActorBase.Direction.LEFT: m_transform.localScale = new Vector3(-m_xScaleBuf , 1 , 1); break;
				case ActorBase.Direction.RIGHT: m_transform.localScale = new Vector3(m_xScaleBuf , 1 , 1); break;
			}

		}
	}
}