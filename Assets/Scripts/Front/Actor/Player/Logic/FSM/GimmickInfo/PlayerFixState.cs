using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerFixState : IPlayerGimmickInfo {

		private ActorBase.Direction m_directionBuf;

		/// <summary>
		/// ステート開始時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnEnter(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("Fix" , true);
			m_directionBuf = arg_player.m_direction;
			arg_player.m_ableJump = false;
		}

		/// <summary>
		/// ステート中の更新
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnUpdate(PlayerComponent arg_player) {
			//プレイヤーの向きは固定である
			arg_player.m_direction = m_directionBuf;
		}

		/// <summary>
		/// ステート終了時
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnExit(PlayerComponent arg_player) {
			arg_player.m_viewer.m_animator.SetBool("Fix" , false);
		}

		/// <summary>
		/// 次に遷移されるステートを返す
		/// </summary>
		/// <param name="arg_player"></param>
		/// <returns></returns>
		public virtual IPlayerGimmickInfo GetNextState(PlayerComponent arg_player) {
			if (!arg_player.Hand.IsGrasping()) { return new PlayerFreeState(); }
			return null;
		}
	}
}