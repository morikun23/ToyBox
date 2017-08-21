using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class InputManager {

		private Player m_player;
		private Hand m_magicHand;

		public void Initialize(MainScene arg_mainScene) {
			m_player = arg_mainScene.m_actorManager.m_player;
			m_magicHand = arg_mainScene.m_actorManager.m_player.m_arm.m_hand;
		}

		public void UpdateByFrame(MainScene arg_mainScene) {
			
			if (Input.GetKey(KeyCode.RightArrow)) {
				m_player.AddTask(new PlayerRunRightCommand(m_player));
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				m_player.AddTask(new PlayerRunLeftCommand(m_player));
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
				m_player.AddTask(new PlayerJumpCommand(m_player));
			}
			if (Input.GetMouseButton(0)) {
				if (true) {
					m_player.AddTask(new PlayerReachCommand(m_player));
					
				}
			}
		}
		
	}
}