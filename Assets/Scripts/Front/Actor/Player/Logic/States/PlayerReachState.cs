//担当：森田　勝
//概要：プレイヤーの射出状態
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerReachState : PlayerStateBase , IPlayerState {

		public int Priority { get { return 3; } }

		protected override bool AbleRun { get { return false; } }

		protected override bool AbleJump { get { return false; } }

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public void OnEnter(Player arg_player) {
			arg_player.m_rigidbody.isKinematic = true;
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
			arg_player.m_rigidbody.isKinematic = false;
		}
	}
}