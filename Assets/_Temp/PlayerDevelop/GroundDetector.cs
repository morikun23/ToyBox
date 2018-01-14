using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class GroundDetector : MonoBehaviour {
		
		[SerializeField]
		private GameObject m_noticeTarget;

		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if(arg_collider.gameObject.layer == LayerMask.NameToLayer("Ground")
				|| arg_collider.gameObject.layer == LayerMask.NameToLayer("Landable")) {
				m_noticeTarget.SendMessage("OnGroundEnter");
			}
		}

		private void OnTriggerExit2D(Collider2D arg_collider) {
			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Ground")
				|| arg_collider.gameObject.layer == LayerMask.NameToLayer("Landable")) {
				m_noticeTarget.SendMessage("OnGroundExit");
			}
		}
	}
}