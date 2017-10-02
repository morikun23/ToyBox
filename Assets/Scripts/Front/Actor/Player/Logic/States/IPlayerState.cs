﻿//担当：森田　勝
//概要：プレイヤーの状態クラスのインターフェイス
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IPlayerState {

		void OnEnter(Player arg_player);
		void OnUpdate(Player arg_player);
		void OnExit(Player arg_player);

		void AddTaskIfAble(Player arg_player,IPlayerCommand arg_command);
	}
}