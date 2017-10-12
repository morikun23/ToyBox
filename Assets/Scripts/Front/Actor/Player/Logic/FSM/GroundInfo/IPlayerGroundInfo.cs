//担当：森田　勝
//概要：地形の状態に依存しているプレイヤーの状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IPlayerGroundInfo {

		/// <summary>
		/// ステート開始
		/// </summary>
		/// <param name="arg_player"></param>
		void OnEnter(PlayerComponent arg_player);

		/// <summary>
		/// ステート更新
		/// </summary>
		/// <param name="arg_player"></param>
		void OnUpdate(PlayerComponent arg_player);

		/// <summary>
		/// ステート終了
		/// </summary>
		/// <param name="arg_player"></param>
		void OnExit(PlayerComponent arg_player);

		/// <summary>
		/// 次に遷移されるステートを返す
		/// </summary>
		/// <param name="arg_player"></param>
		/// <returns></returns>
		IPlayerGroundInfo GetNextState(PlayerComponent arg_player);

	}
}