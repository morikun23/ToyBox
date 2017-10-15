using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PortableItem : Item {

		public override void OnGraspedEnter(PlayerComponent arg_player) {
			arg_player.Arm.m_shorten = true;
			SetAbleRelease (false);
			SetAbleGrasp (false);
		}

		public override void OnGraspedStay(PlayerComponent arg_player) {
			this.m_transform.position = arg_player.Arm.GetTopPosition();
			if (arg_player.GetCurrentState() != typeof(PlayerReachState)) {
				arg_player.m_rigidbody.isKinematic = false;
			}

			if (arg_player.Arm.m_lengthBuf.Count == 0) {
				SetAbleRelease (true);
			}
		}

		public override void OnGraspedExit(PlayerComponent arg_player) {
			SetAbleGrasp (true);
			SetAbleRelease (false);
			arg_player.m_inputHandle.m_reach = false;
		}

		public override bool IsAbleGrasp (){
			return m_flg_ableGrasp;
		}

		public override bool IsAbleRelease (){
			return m_flg_ableReleace;
		}
	}
}