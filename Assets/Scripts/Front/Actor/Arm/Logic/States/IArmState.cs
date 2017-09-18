using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IArmState {

		void OnEnter(Arm arg_arm);

		void OnUpdate(Arm arg_arm);

		void OnExit(Arm arg_arm);

		IArmState GetNextState(Arm arg_arm);
	}
}