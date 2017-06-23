using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Gear {
	public class GearGenerate : GearTask {

		public void OnEnter(Gear gear) {
			
		}

		public void OnUpdate(Gear gear) {
			if (true) {
				gear.TaskTransition(new GearMove());
			}
		}

		public void OnExit(Gear gear) {

		}
	}
}