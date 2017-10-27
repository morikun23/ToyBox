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

		//カメラ
		[SerializeField]
		private Camera m_uiCamera;
        

		public void Initialize() {

			m_playable = FindObjectOfType<Playable>();

			m_buttons = FindObjectsOfType<Button>().ToList();
			foreach(Button button in m_buttons) {
				button.Initialize();
				button.SetControlTarget(m_playable);
				button.SwitchCamera(m_uiCamera);
			}
            
        }

		public void UpdateByFrame() {
            

			if (Input.GetMouseButtonDown(0)) {
				Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 worldPointUI = m_uiCamera.ScreenToWorldPoint (Input.mousePosition);

                //タッチをした位置にオブジェクトがあるかどうかを判定
                RaycastHit2D hit = Physics2D.CircleCast(worldPoint, 0.5f, Vector2.zero , 0.1f , 1 << LayerMask.NameToLayer("Item") | 1 << LayerMask.NameToLayer("PotableItem_Ground"));
				RaycastHit2D hitUI = Physics2D.CircleCast (worldPointUI,0.5f,Vector2.zero,0.1f,1 << LayerMask.NameToLayer("UI"));

				if (m_playable.m_playableHand.IsGrasping() && !hitUI.collider) {
					m_playable.Release();
				}
				else {
					if (hit && !hitUI.collider) {
						Item item = hit.collider.GetComponent<Item>();
						m_playable.ReachOutFor(item);
					}
				}

			}

			foreach (Button button in m_buttons) {
				button.UpdateByFrame();
			}
		}


        //入力受付関数
        public void InputStart()
        {
            foreach (Button button in m_buttons)
            {
                button.TouchEnd();
                button.enabled = true;
            }
        }

        //入力禁止関数
        public void InputStop()
        {
            foreach (Button button in m_buttons)
            {
                button.TouchEnd();
                button.enabled = false;
            }
        }

	}
}