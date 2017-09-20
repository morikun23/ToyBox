using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerController : ObjectBase {

		[SerializeField]
		private Player m_player;
		
		private Playable m_playable;

		[SerializeField]
		private PlayableArm m_arm;

		[SerializeField]
		private IPlayableHand m_hand;

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

			m_playable = m_player;
			m_arm = m_player.Arm;
			m_hand = m_player.Hand;
		}

		public void UpdateByFrame() {

			if (Input.GetKey(KeyCode.LeftArrow)) {
				m_player.m_direction = ActorBase.Direction.LEFT;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				m_player.m_direction = ActorBase.Direction.RIGHT;
			}

			if (Input.GetMouseButtonDown(0)) {
				Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				//タッチをした位置にオブジェクトがあるかどうかを判定
				RaycastHit2D hit = Physics2D.Raycast(worldPoint , Vector2.zero,0.1f,1 << LayerMask.NameToLayer("Item"));

				if (hit) {
					Item item = hit.collider.gameObject.GetComponent<Item>();
					#region アイテムに向けて手を伸ばすための処理（ほぼテンプレ）
					m_hand.SetItemBuffer(item);
					m_arm.SetTargetPosition(item.m_transform.position);
					m_player.m_inputHandle.m_reach = true;
					#endregion
				}
				else {
					m_hand.Release();
				}
			}

			m_playable.m_inputHandle.m_run = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

			m_playable.m_inputHandle.m_jump = Input.GetKey(KeyCode.Space);

			m_player.UpdateByFrame();
		}

		private void PlayerGenerate() {
			Instantiate(Resources.Load<Player>("Actor/Player/Player") , m_transform);
		}

	}
}