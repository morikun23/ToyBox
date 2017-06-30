using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public interface IPlayerCommand {

		void OnEnter(Player arg_player);
		void OnUpdate(Player arg_player);
		bool IsFinished();
	}
}