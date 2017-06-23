using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI {
	public class Player : MonoBehaviour {

		Rigidbody2D rb2d;

		public void Initialize() {
			rb2d = GetComponent<Rigidbody2D>();
		}

		public void UpdateByFrame() {

		}

		public void Jump() {
			rb2d.AddForce(Vector2.up * 200);
		}
	}
}