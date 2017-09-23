using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class StartPoint : MonoBehaviour {

		public virtual void Initialize(PlayerComponent arg_player) {

		}

		public virtual void Generate(PlayerComponent arg_player) {
			arg_player.m_transform.position = this.transform.position;
		}
	}
}