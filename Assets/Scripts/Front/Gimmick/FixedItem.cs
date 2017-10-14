using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class FixedItem : Item {

		public override void OnGraspedEnter(PlayerComponent arg_player) {

		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
		}

		public override bool IsAbleGrasp (){
			throw new System.NotImplementedException ();
			return m_flg_ableGrasp;
		}

		public override bool IsAbleRelease (){
			throw new System.NotImplementedException ();
			return m_flg_ableReleace;
		}
	}
}