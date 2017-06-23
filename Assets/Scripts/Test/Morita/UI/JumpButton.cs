using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI {
	public class JumpButton : MonoBehaviour {

		public void OnClick() {
			FindObjectOfType<Player>().Jump();
		}
	}
}