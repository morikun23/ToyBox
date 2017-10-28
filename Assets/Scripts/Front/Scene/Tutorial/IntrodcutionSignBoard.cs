using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class IntrodcutionSignBoard : Signboard {

		public GameObject m_animation;

		//この看板は一度でも読まれたか（進行を制御するためのフラグ）
		private bool m_isRead;

		public override void OnGraspedEnter(PlayerComponent arg_player) {
			base.OnGraspedEnter(arg_player);
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			base.OnGraspedStay(arg_player);
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
			base.OnGraspedExit(arg_player);
			m_isRead = true;
		}

		public bool IsRead() {
			return m_isRead;
		}
	}
}