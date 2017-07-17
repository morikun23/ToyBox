using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public interface IPlayerState {

		void OnEnter(Player arg_player);
		void OnUpdate(Player arg_player);
		void OnExit(Player arg_player);

		void AddTask(Player arg_player,IPlayerCommand arg_command);
	}
}