using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.View {
	public class ActorManager : MonoBehaviour {

		private Player m_player;

		public void Initialize(Logic.ActorManager arg_logic) {
			m_player = Instantiate(Resources.Load<GameObject>("Actor/Player") , this.transform).GetComponent<Player>();
			m_player.Initialize(arg_logic.m_player);
		}

		public void UpdateByFrame(Logic.ActorManager arg_logic) {
			m_player.UpdateByFrame(arg_logic.m_player);
		}
	}
}