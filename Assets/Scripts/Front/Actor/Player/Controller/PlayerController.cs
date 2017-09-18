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
			m_arm = FindObjectOfType<Arm>();

			if (!m_player) { PlayerGenerate(); }
			m_player.Initialize();
			m_arm.Initialize(m_player);
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
					ItemGrab(item);
				}
			}


			m_player.m_inputHandle.m_run = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

			m_player.m_inputHandle.m_jump = Input.GetKey(KeyCode.Space);

			if (!m_arm.lengthen) {
				m_player.m_inputHandle.m_reach = false;
			}

			m_player.UpdateByFrame();
			m_arm.UpdateByFrame(m_player);
		}

		private void PlayerGenerate() {
			Instantiate(Resources.Load<Player>("Actor/Player/Player") , m_transform);
		}

		private void ItemGrab(Item arg_item) {
			Debug.Log("S");
			m_player.m_inputHandle.m_reach = true;
			m_arm.SetTargetPosition(arg_item.m_transform.position);
			m_arm.lengthen = true;
			
		}
	}
}