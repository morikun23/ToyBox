//担当：森田　勝
//概要：プレイヤーの移動状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerGroundedRunState : OnPlayerGroundedState {
		
		private ActorBase.Direction m_directionBuf;

		public PlayerGroundedRunState(ActorBase.Direction m_direction) {
			this.m_directionBuf = m_direction;
		}

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnEnter(Player arg_player) {
			arg_player.m_direction = m_directionBuf;
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnUpdate(Player arg_player) {

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
		public override void OnExit(Player arg_player) {
			
		}

		public override IPlayerState GetNextState(Player arg_player) {
			if (arg_player.m_inputHandle.m_reach) { return new PlayerReachState(this); }
			if (arg_player.m_inputHandle.m_jump) {
				return new PlayerGroundedJumpState();
			}

			if (arg_player.m_inputHandle.m_run) {
				if (m_directionBuf != arg_player.m_direction) {
					return new PlayerGroundedRunState(arg_player.m_direction);
				}
			}
			else {
				return new PlayerGroundedIdleState();
			}

			return null;
		}

	}
}