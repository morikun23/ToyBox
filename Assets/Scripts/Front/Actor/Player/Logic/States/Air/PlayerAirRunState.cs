using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerAirRunState : OnPlayerAirState {

		private ActorBase.Direction m_directionBuf;

		public PlayerAirRunState(ActorBase.Direction m_direction) {
			this.m_directionBuf = m_direction;
		}

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnEnter(PlayerComponent arg_player) {
			arg_player.m_direction = m_directionBuf;
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public override void OnUpdate(PlayerComponent arg_player) {

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
		public override void OnExit(PlayerComponent arg_player) {

		}

		public override IPlayerState GetNextState(PlayerComponent arg_player) {
			if (arg_player.m_inputHandle.m_reach) { return new PlayerReachState(this); }
			if (arg_player.m_isGrounded) {
				return new OnPlayerGroundedState().GetNextState(arg_player);
			}

			if (arg_player.m_inputHandle.m_run) {
				if (m_directionBuf != arg_player.m_direction) {
					return new PlayerAirRunState(arg_player.m_direction);
				}
			}
			else {
				return new PlayerAirIdleState();
			}

			return null;
		}
	}
}