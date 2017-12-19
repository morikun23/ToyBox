using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PortableItem : Item {

		Rigidbody2D rig_;



		public override void OnGraspedEnter(PlayerComponent arg_player) {
			arg_player.Arm.m_shorten = true;
			SetAbleRelease (false);
			SetAbleGrasp (false);
			rig_ = GetComponent<Rigidbody2D>();
			rig_.simulated = false;

            //新しい音でして
            AudioManager.Instance.QuickPlaySE("SE_PlayerHand_grab");
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			
			if (arg_player.GetCurrentState() != typeof(PlayerReachState)) {
				arg_player.m_rigidbody.isKinematic = false;

			}

			if (arg_player.Arm.m_lengthBuf.Count == 0) {
				this.m_transform.position = new Vector3 (arg_player.Arm.GetTopPosition ().x,
					arg_player.Arm.GetTopPosition ().y + 0.5f, 0);
				SetAbleRelease (true);
			} else {
				this.m_transform.position = arg_player.Arm.GetTopPosition ();
			}
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
			SetAbleGrasp (true);
			SetAbleRelease (false);
			arg_player.m_inputHandle.m_reach = false;
			rig_ = GetComponent<Rigidbody2D>();
			rig_.simulated = true;
		}

		public override bool IsAbleGrasp (){
			return m_flg_ableGrasp;
		}

		public override bool IsAbleRelease (){
			return m_flg_ableReleace;
		}
	}
}