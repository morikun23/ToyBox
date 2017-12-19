//担当：森田　勝
//概要：プレイヤーの移動状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerRunState : IPlayerState {

		private ActorBase.Direction m_directionBuf;
		private int m_cnt_move = 0;

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnEnter(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("Run" , true);

			m_directionBuf = arg_player.m_direction;

			AudioManager.Instance.RegisterSE ("Foot", "SE_Player_Walk");
			AudioManager.Instance.PlaySE ("Foot",true);
			
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnUpdate(PlayerComponent arg_player) {

			//行き止まりである
			if (IsDeadEnd(arg_player)) {
				return;
			}
			arg_player.m_rigidbody.velocity = new Vector3() {
				x = arg_player.m_speed * (int)arg_player.m_direction ,
				y = arg_player.m_rigidbody.velocity.y ,
				z = 0
			};


		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnExit(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("Run" , false);
			arg_player.m_rigidbody.velocity = new Vector3() {
				x = 0 ,
				y = arg_player.m_rigidbody.velocity.y ,
				z = 0
			};
			AudioManager.Instance.StopSE ("Foot");
			AudioManager.Instance.ReleaseSE ("Foot");
		}

		public virtual IPlayerState GetNextState(PlayerComponent arg_player) {
			if (arg_player.m_inputHandle.m_reach) { return new PlayerReachState(this); }
			if (arg_player.m_inputHandle.m_jump && arg_player.m_ableJump) { return new PlayerJumpState(); }
			if(!arg_player.m_inputHandle.m_run) { return new PlayerIdleState(); }

			if(arg_player.m_direction != m_directionBuf) { return new PlayerRunState(); }

			return null;
		}

		private bool IsDeadEnd(PlayerComponent arg_player) {
			if (Physics2D.BoxCast(arg_player.m_transform.position ,
				new Vector2(arg_player.m_body.bounds.size.x , 1) ,
				0 ,new Vector2((int)arg_player.m_direction , 0) ,
				0.1f , 1 << LayerMask.NameToLayer("Ground"))) {
				return true;
			}
			return false;
		}

	}
}