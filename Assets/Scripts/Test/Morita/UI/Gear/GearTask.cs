using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Gear {
	public interface GearTask {

		void OnEnter(Gear gear);

		void OnUpdate(Gear gear);

		void OnExit(Gear gear);
		
	}
}