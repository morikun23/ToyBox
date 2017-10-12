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
			foreach (Button button in m_buttons) {
				button.UpdateByFrame();
			}
		}
	}
}