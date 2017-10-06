using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerFreeState : IPlayerGimmickInfo {

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

		/// <summary>
		/// 次に遷移されるステートを返す
		/// </summary>
		/// <param name="arg_player"></param>
		/// <returns></returns>
		public virtual IPlayerGimmickInfo GetNextState(PlayerComponent arg_player) {
			if (!arg_player.Hand.IsGrasping()) { return null; }

			System.Type itemType = arg_player.Hand.m_graspingItem.GetType();
			if (itemType == typeof(PortableItem)) { return new PlayerCarryState(); }
			if(itemType.IsSubclassOf(typeof(FixedItem))) { return new PlayerFixState(); }
			return null;
		}
	}
}