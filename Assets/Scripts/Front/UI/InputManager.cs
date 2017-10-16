using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox {
	public class InputManager : MonoBehaviour {

		//ボタン
		private List<Button> m_buttons;

		//操作キャラ
		private Playable m_playable;

		// Use this for initialization
		void Start() {

			m_playable = FindObjectOfType<Playable>();

			m_buttons = FindObjectsOfType<Button>().ToList();
			foreach(Button button in m_buttons) {
				button.Initialize();
				button.SetControlTarget(m_playable);
			}
		}

		// Update is called once per frame
		void Update() {

			if (Input.GetMouseButtonDown(0)) {
				Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				//タッチをした位置にオブジェクトがあるかどうかを判定
				RaycastHit2D hit = Physics2D.Raycast(worldPoint , Vector2.zero , 0.1f , 1 << LayerMask.NameToLayer("Item"));

				if (m_playable.m_playableHand.IsGrasping()) {
					m_playable.Release();
				}
				else {
					if (hit) {
						Item item = hit.collider.GetComponent<Item>();
						m_playable.ReachOutFor(item);
					}
				}

			}

			foreach (Button button in m_buttons) {
				button.UpdateByFrame();
			}
		}
	}
}