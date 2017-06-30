using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox.Logic {
	public class ActorManager {
		
		private List<ActorGimmick> m_actorGimmicks;
		public Player m_player { get; private set; }

		public void Initialize() {
			m_player = new Player();
			m_player.Initialize();
			m_actorGimmicks = new List<ActorGimmick>();
		}

		public void UpdateByFrame() {
			m_player.UpdateByFrame();
		}
		
	}
}