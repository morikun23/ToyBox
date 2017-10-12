using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerIdleState : IPlayerState {

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnEnter(PlayerComponent arg_player) {

		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnUpdate(PlayerComponent arg_player) {

		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnExit(PlayerComponent arg_player) {

		}

		public virtual IPlayerState GetNextState(PlayerComponent arg_player) {
			if (arg_player.Hand.IsGrasping()) {
			
				if(arg_player.Hand.m_graspingItem.GetType().IsSubclassOf(typeof(FixedItem))) {
					//動けないアイテムをつかんでいるときは静止状態から遷移しない
					return null;
				}
			}
			if (arg_player.m_inputHandle.m_reach) { return new PlayerReachState(this); }
			if (arg_player.m_inputHandle.m_run) { return new PlayerRunState(); }
			if (arg_player.m_inputHandle.m_jump && arg_player.m_ableJump) { return new PlayerJumpState(); }
			return null;
		}
	}
}