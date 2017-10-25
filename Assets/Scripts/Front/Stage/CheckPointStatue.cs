﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class CheckPointStatue : StartPoint {

		[SerializeField]
		private Animator m_animator;

		// Use this for initialization
		private void Start() {
			if (m_animator == null) {
				m_animator = GetComponentInChildren<Animator>();
			}
			m_isActive = false;
		}

		// Update is called once per frame
		private void Update() {

		}

		protected override void OnTriggerEnter2D(Collider2D arg_collider) {
			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
				m_animator.SetBool("Active" , true);
			}
			base.OnTriggerEnter2D(arg_collider);
		}
	}
}