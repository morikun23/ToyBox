//担当：森田　勝
//概要：プレイヤーの状態クラスのインターフェイス
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IPlayerState {

		void OnEnter(PlayerComponent arg_player);
		void OnUpdate(PlayerComponent arg_player);
		void OnExit(PlayerComponent arg_player);

		/// <summary>
		/// 次に遷移されるステートを返す
		/// </summary>
		/// <param name="arg_layer"></param>
		/// <returns></returns>
		IPlayerState GetNextState(PlayerComponent arg_player);

	}
}