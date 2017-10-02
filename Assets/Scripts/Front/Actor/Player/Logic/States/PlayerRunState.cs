//担当：森田　勝
//概要：プレイヤーの移動状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerRunState : IPlayerState {

		public PlayerRunState(ActorBase.Direction m_direction) {
			
		}

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

			if (Physics2D.BoxCast(arg_player.m_transform.position ,
				arg_player.m_collider.size , 0 ,
				new Vector2((int)arg_player.m_direction , 0) ,
				0.1f , 1 << LayerMask.NameToLayer("Ground"))) {
				return;
			}

			arg_player.m_transform.position += new Vector3() {
				x = arg_player.m_speed * (int)arg_player.m_direction ,
				y = 0 ,
				z = 0
			};
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnExit(PlayerComponent arg_player) {

		}

		public virtual IPlayerState GetNextState(PlayerComponent arg_player) {
			if (arg_player.m_inputHandle.m_reach) { return new PlayerReachState(this); }
			if (arg_player.m_inputHandle.m_jump) { return new PlayerJumpState(); }
			if(!arg_player.m_inputHandle.m_run) { return new PlayerIdleState(); }
			return null;
		}

	}
}