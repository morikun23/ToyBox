using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Gear {
	public class Gear : MonoBehaviour {

		public bool isActive;

		public GearTask gearTask;

		public void Initialize() {
			isActive = false;
			gearTask = new GearIdle();
			gearTask.OnEnter(this);
		}

		public void UpdateByFrame() {
			
			gameObject.SetActive(isActive);

			if (isActive) {
				gearTask.OnUpdate(this);
			}
		}

		public void Show() {
			TaskTransition(new GearGenerate());
			isActive = true;
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			pos.z = 0;
			transform.position = pos;
		}
		
		public void Hide(){
			isActive = false;
		}

		public void TaskTransition(GearTask nextTask) {
			this.gearTask.OnExit(this);
			this.gearTask = nextTask;
			this.gearTask.OnEnter(this);
		}
	}
}