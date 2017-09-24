using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class GoalPoint : CheckPoint {

		public void OnTriggerEnter2D(Collider2D arg_collider) {
			if (LayerMask.LayerToName(arg_collider.gameObject.layer) == "Player") {
				base.m_isActive = true;
			}
		}
	}
}