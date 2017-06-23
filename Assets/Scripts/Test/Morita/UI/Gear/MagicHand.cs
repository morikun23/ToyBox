using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Gear {
	public class MagicHand : MonoBehaviour {

		private Player player;

		private Vector2 destination;

		private int t;

		private bool isFire;

		public void Initialize() {
			player = FindObjectOfType<Player>();
			isFire = false;
			t = 0;
		}

		public void UpdateByFrame() {
			if (isFire) {
				if (t < 10) {
					Vector2 velocity = destination - (Vector2)player.transform.position / 10;
					transform.position += (Vector3)velocity;
					t++;
				}
			}
			else {
				transform.position = player.transform.position;
			}
		}

		public void Fire(Vector2 pos) {
			destination = pos;
			isFire = true;
		}
	}
}