using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class StartPoint : CheckPoint {

		/// <summary>
		/// プレイヤーの生成を行う
		/// ここに生成の演出を加えても良い
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void Generate(PlayerComponent arg_player) {

			if (arg_player == null) return;

			arg_player.m_transform.position = this.transform.position;
			arg_player.gameObject.SetActive(true);
		}
	}
}