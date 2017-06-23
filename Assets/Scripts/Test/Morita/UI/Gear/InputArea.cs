using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Gear {
	public class InputArea : MonoBehaviour {

		private float touchedTime;
		
		public void Initialize(Gear arg_gear) {
			touchedTime = 0;
		}

		public void UpdateByFrame(Gear arg_gear) {
			foreach (Touch touchInfo in Input.touches) {
				if (arg_gear.isActive) {
					return;
				}
				switch (touchInfo.phase) {
					case TouchPhase.Began:
					touchedTime = 0;
					break;
					case TouchPhase.Stationary:
					touchedTime += Time.deltaTime;

					if (touchedTime >= 1.0f) {
						//タッチ座標はスクリーン座標なので、マウスと同じ扱いの座標に直す必要がある
						Vector2 tapPoint = Camera.main.ScreenToWorldPoint(touchInfo.position);
						
						if (Physics2D.OverlapPoint(tapPoint)) {
							
							if (Physics2D.Raycast(tapPoint , Vector2.down)) {
								//ギミックとヒットした際はギアを出さない
							}
							
						}
						else {
							arg_gear.Show();
						}
					}

					break;
					case TouchPhase.Moved: break;
					case TouchPhase.Ended:
					arg_gear.TaskTransition(new GearRemove());
					break;
					case TouchPhase.Canceled:
					arg_gear.TaskTransition(new GearRemove());
					break;
				}
				break;
			}
		}

		public void OnDebug() {
			Debug.Log("enter");
		}
	}
}