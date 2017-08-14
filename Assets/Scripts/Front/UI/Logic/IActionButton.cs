using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic.UI {
	public interface IActionButton {

		void OnPointerDown();
		void OnPointerUp();

	}
}