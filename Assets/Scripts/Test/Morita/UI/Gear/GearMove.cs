using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Gear {
	public class GearMove : GearTask {

		public void OnEnter(Gear gear) {

		}

		public void OnUpdate(Gear gear) {
			//指の動きを見る
			foreach (Touch touchInfo in Input.touches) {
				float xAngle = 0;
				float yAngle = 0;
				float zAngle = 0;
				
				if (gear.transform.eulerAngles.z >= 0 && gear.transform.eulerAngles.z < 45 && gear.transform.eulerAngles.z >= 315) {
					//左に入るまで
					zAngle = touchInfo.deltaPosition.x;
				}
				else if (gear.transform.eulerAngles.z >= 45 && gear.transform.eulerAngles.z < 135) {
					zAngle = touchInfo.deltaPosition.y;
				}
				else if (gear.transform.eulerAngles.z >= 135 && gear.transform.eulerAngles.z < 225) {
					zAngle = touchInfo.deltaPosition.x;
				}
				else if (gear.transform.eulerAngles.z >= 225 && gear.transform.eulerAngles.z < 315) {
					zAngle = touchInfo.deltaPosition.y;
				}

				//移動量に応じて角度計算


				//回転
				gear.transform.Rotate(xAngle , yAngle , -zAngle , Space.World);

				break;
			}

		}
		
		public void OnExit(Gear gear) {

		}
	}
}