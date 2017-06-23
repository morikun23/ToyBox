using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita {
	public class TestScene : MonoBehaviour {

		Logic.PlayerController m_playerController;

		private View.Player m_viewPlayer;
		private View.MagicHand m_viewHand;
		private View.Arm m_viewArm;


		// Use this for initialization
		void Start() {

			m_playerController = new Logic.PlayerController();
			m_playerController.Initialize();

			m_viewPlayer = new GameObject("Player").AddComponent<View.Player>();
			m_viewPlayer.Initialize(m_playerController.m_player);

			m_viewHand = new GameObject("MagicHand").AddComponent<View.MagicHand>();
			m_viewHand.Initialize(m_playerController.m_player.m_magicHand);

			m_viewArm = new GameObject("Arm").AddComponent<View.Arm>();
			m_viewArm.Initialize(m_playerController.m_player.m_arm);

			InputManager.Instance.Initialize();
		}

		void Update() {
			m_playerController.UpdateByFrame();
			m_viewPlayer.UpdateByFrame(m_playerController.m_player);
			m_viewHand.UpdateByFrame(m_playerController.m_player.m_magicHand);
			m_viewArm.UpdateByFrame(m_playerController.m_player.m_arm);
			InputManager.Instance.UpdateByFrame();
		}
	}
}