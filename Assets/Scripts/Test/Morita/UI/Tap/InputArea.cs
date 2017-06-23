using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Tap {
	public class InputArea : MonoBehaviour {

		[SerializeField]
		private Lever.Direction direction;

		public bool isActive { get; private set; }

		public void Initialize() {
			isActive = false;
		}

		public void UpdateByFrame(Lever lever) {
			if (isActive) {
				lever.SetDirection(direction);
			}
		}

		public void OnPointerDown() {
			isActive = true;
		}

		public void OnPointerUp() {
			isActive = false;
		}
	}
}