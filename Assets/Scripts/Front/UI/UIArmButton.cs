using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ToyBox {
	public class UIArmButton : UIDraggableButton {

		protected override void OnPressed() {
			base.OnPressed();
		}

		protected override void OnReleased() {
			base.OnReleased();
			transform.position = DefaultPosition;
		}
	}
}