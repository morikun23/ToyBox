using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerController : ObjectBase {

		[SerializeField]
		private Player m_player;

		[SerializeField]
		private Arm m_arm;

		[SerializeField]
		private Hand m_hand;

		void Start() {
			Initialize();
		}

		void Update() {
			UpdateByFrame();
		}

		public void Initialize() {
			m_player = FindObjectOfType<Player>();
			if (!m_player) { PlayerGenerate(); }
			m_player.Initialize();
		}

		public void UpdateByFrame() {

			if (Input.GetKey(KeyCode.LeftArrow)) {
				m_player.m_direction = ActorBase.Direction.LEFT;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				m_player.m_direction = ActorBase.Direction.RIGHT;
			}

			m_player.m_inputHandle.m_run = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
			m_player.m_inputHandle.m_jump = Input.GetKey(KeyCode.Space);
			}

			m_player.UpdateByFrame();
		}

		private void PlayerGenerate() {
			Instantiate(Resources.Load<Player>("Actor/Player/Player") , m_transform);
		}

	}
}