using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ImmobilizedItem : Item {


		public override void OnGraspedEnter(PlayerComponent arg_player) {

            //新しい音でございます
            AudioManager.Instance.QuickPlaySE("SE_PlayerHand_grab");
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			SetAbleGrasp (false);
			SetAbleRelease (true);
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
			arg_player.Arm.m_shorten = true;
			SetAbleGrasp (true);
			SetAbleRelease (false);
		}


		public override bool IsAbleGrasp (){
			return m_flg_ableGrasp;
		}

		public override bool IsAbleRelease (){
			return m_flg_ableReleace;
		}
	}
}