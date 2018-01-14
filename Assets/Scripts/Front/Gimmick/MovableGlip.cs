using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;
using System;

namespace ToyBox {
	public class MovableGlip : Item {

		public override GraspedReaction Reaction {
			get {
				return GraspedReaction.PULL_TO_ITEM;
			}
		}

		private Vector3 m_direction;

		public enum GripState{
			Neutoral,
			ENTER,
			STAY,
			EXIT
		}
		public GripState m_enu_state;
		
		public override void OnGraspedEnter(Player arg_player) {
			m_enu_state = GripState.ENTER;

			SetAbleGrasp (false);
			SetAbleRelease (false);

			//新しい音でございます
			AudioManager.Instance.QuickPlaySE("SE_PlayerHand_grab");
			arg_player.RigidbodyComponent.velocity = Vector2.zero;

			m_enu_state = GripState.ENTER;

		}

		public override void OnGraspedStay(Player arg_player) {
			arg_player.RigidbodyComponent.isKinematic = true;
			
			if (m_enu_state == GripState.ENTER) {
				if (!arg_player.PlayableArm.IsUsing()) {
					SetAbleRelease(true);
					m_enu_state = GripState.STAY;
				}
			}
			else if(m_enu_state == GripState.STAY) {
				arg_player.PlayableArm.TopPosition = this.transform.position;
				arg_player.PlayableArm.BottomPosition = this.transform.position;
			}
		}

		public override void OnGraspedExit(Player arg_player) {
			m_enu_state = GripState.Neutoral;
			base.OnGraspedExit(arg_player);
			SetAbleGrasp (true);
			SetAbleRelease (false);
			arg_player.RigidbodyComponent.isKinematic = false;
		}

	}
}