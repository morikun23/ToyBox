//担当：森田　勝
//概要：プレイヤーの移動状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerRunState : PlayerStateBase , IPlayerState{
		
		public int Priority { get { return 1; } }


		private ActorBase.Direction m_directionBuf;

		public PlayerRunState(ActorBase.Direction arg_direction) {
			m_directionBuf = arg_direction;
		}

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnEnter(Player arg_player) {
			arg_player.m_direction = m_directionBuf;
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnUpdate(Player arg_player) {
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
		public void OnExit(Player arg_player) {
			
		}

	}
}