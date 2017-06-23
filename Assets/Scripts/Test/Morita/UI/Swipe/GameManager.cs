using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Swipe {
	public class GameManager : MonoBehaviour {

		[SerializeField]
		Lever lever;

		[SerializeField]
		InputArea inputArea;

		Player player;

		// Use this for initialization
		void Start() {

			lever.Initialize();
			inputArea.UpdateByFrame(lever);
			player = FindObjectOfType<Player>();
			player.Initialize();

		}

		// Update is called once per frame
		void Update() {

			inputArea.UpdateByFrame(lever);
			player.UpdateByFrame();

			if (!inputArea.isActive) {
				lever.SetDirection(Lever.Direction.NUTRAL);
			}
			lever.UpdateByFrame();

		}
	}
}