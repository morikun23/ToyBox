//担当：森田　勝
//概要：プレイヤーの静止状態のクラス
//参考：なし

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerIdleState : PlayerStateBase , IPlayerState{

		public int Priority { get { return 0; } }

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnEnter(Player arg_player) {
			
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnUpdate(Player arg_player) {

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnExit(Player arg_player) {
			
		}
		
	}
}