using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public interface IArmAction {

		void OnUpdate(Arm arg_arm);

		bool IsFinished();
	}
}