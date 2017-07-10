using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class MagicHandGrab : IMagicHandAction {
		
		public void OnUpdate(MagicHand arg_magicHand) {
			//TODO:ギミックオブジェクトとのあたり判定を行う
			
		}

		public bool IsFinished() {
			return true;
		}
	}
}