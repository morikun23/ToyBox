using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerController : ObjectBase {

		private Playable m_playable;

		private PlayableArm m_arm;

		private IPlayableHand m_hand;

		void Start() {
			Initialize();
		}

		void Update() {
			UpdateByFrame();
		}

		public void Initialize() {
			m_playable = FindObjectOfType<Playable>();
			m_arm = m_playable.m_playableArm;
			m_hand = m_playable.m_playableHand;
		}

		public void UpdateByFrame() {

			if (Input.GetKey(KeyCode.LeftArrow)) {
				m_playable.m_direction = ActorBase.Direction.LEFT;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				m_playable.m_direction = ActorBase.Direction.RIGHT;
			}

			if (Input.GetMouseButtonDown(0)) {
				Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				//タッチをした位置にオブジェクトがあるかどうかを判定
				RaycastHit2D hit = Physics2D.Raycast(worldPoint , Vector2.zero,0.1f,1 << LayerMask.NameToLayer("Item"));

				if (m_hand.IsGrasping()) {
					m_playable.Release();
				}
				else {
					if (hit) {
						Item item = hit.collider.GetComponent<Item>();
						m_playable.ReachOutFor(item);
					}
				}

			}

			m_playable.m_inputHandle.m_run = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

			m_playable.m_inputHandle.m_jump = Input.GetKey(KeyCode.Space);
		}

	

	}
}