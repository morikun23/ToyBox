using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public interface IMagicHandAction {
		
		void OnUpdate(MagicHand arg_magicHand);
		
		bool IsFinished();
	}
}