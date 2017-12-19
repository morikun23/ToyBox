using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class CheckPointStatue : StartPoint {

		private Animator m_animator;

		// Use this for initialization
		private void Start() {
			m_animator = GetComponentInChildren<Animator>();
			m_isActive = false;
		}

		// Update is called once per frame
		private void Update() {

		}

		protected override void OnTriggerEnter2D(Collider2D arg_collider) {
			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
				if (!m_isActive) {
					m_animator.SetBool("Active" , true);

                    //音でございまして
                    AudioManager.Instance.QuickPlaySE("SE_SavePoint");
				}
			}
			base.OnTriggerEnter2D(arg_collider);
		}
	}
}