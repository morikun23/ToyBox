//担当：森田　勝
//概要：プレイヤーの静止状態のクラス
//参考：なし

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerGroundedIdleState : OnPlayerGroundedState {

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnEnter(Player arg_player) {
			
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnUpdate(Player arg_player) {

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnExit(Player arg_player) {
			
		}

		public override IPlayerState GetNextState(Player arg_player) {
			if (arg_player.m_inputHandle.m_reach) { return new PlayerReachState(this); }
			if (arg_player.m_inputHandle.m_run) { return new PlayerGroundedRunState(arg_player.m_direction); }
			if (arg_player.m_inputHandle.m_jump) { return new PlayerGroundedJumpState(); }

			return null;
		}
		
	}
}