using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita {
	public class TestScene : MonoBehaviour {

		Player player;
		PlayerController pc;

		// Use this for initialization
		void Start() {
			player = FindObjectOfType<Player>();
			player.Initialize();
			pc = new PlayerController();
			pc.Initialize();
		}

		// Update is called once per frame
		void Update() {
			player.UpdateByFrame();
			pc.UpdateByFrame();
		}
	}
}