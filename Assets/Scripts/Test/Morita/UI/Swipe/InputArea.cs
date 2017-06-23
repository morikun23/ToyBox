using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Swipe {
	public class InputArea : MonoBehaviour {

		public bool isActive { get; private set; }

		public void Initialize() {
			isActive = false;
		}

		public void UpdateByFrame(Lever lever) {
			if (isActive) {
				foreach (Touch touchInfo in Input.touches) {
					if (touchInfo.phase == TouchPhase.Moved) {
						float h = touchInfo.deltaPosition.x;
						if (Mathf.Abs(h) > 10) {
							if (h > 0) {
								lever.SetDirection(Lever.Direction.RIGHT);
							}
							else if (h < 0) {
								lever.SetDirection(Lever.Direction.LEFT);
							}
						}
					}
					break;
				}
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