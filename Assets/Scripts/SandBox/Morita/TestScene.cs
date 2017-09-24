using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita {
	public class TestScene : MonoBehaviour {

		Player player;

		// Use this for initialization
		void Start() {
			player = FindObjectOfType<Player>();
			player.Initialize();
		}

		// Update is called once per frame
		void Update() {
			player.UpdateByFrame();
		}
	}
}