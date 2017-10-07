using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerAirIdleState : OnPlayerAirState {

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnEnter(PlayerComponent arg_player) {

		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnUpdate(PlayerComponent arg_player) {

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnExit(PlayerComponent arg_player) {

		}

		public override IPlayerState GetNextState(PlayerComponent arg_player) {
			if (arg_player.m_inputHandle.m_reach) { return new PlayerReachState(this); }
			if (arg_player.m_isGrounded) {
				return new OnPlayerGroundedState().GetNextState(arg_player);
			}
			if (arg_player.m_inputHandle.m_run) {
				return new PlayerAirRunState(arg_player.m_direction);
			}
			return null;
		}
	}
}