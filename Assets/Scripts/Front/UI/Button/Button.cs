 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class Button : ToyBox.MobileInput {

		protected Playable m_playable;

		protected Animator m_animator;

		private bool m_isPressing;

#if UNITY_EDITOR && DEVELOP
		[SerializeField]
		protected KeyCode m_key;
#endif

		public virtual void Initialize() {
			m_isPressing = false;
			m_animator = GetComponent<Animator>();
		}

		public virtual void UpdateByFrame() {
			if (m_isPressing) { this.OnPress(); }
		}

		public sealed override void Started() {
			base.Started();
			m_isPressing = true;
			this.OnDown();
		}

		public sealed override void TouchEnd() {
			base.TouchEnd();
			m_isPressing = false;
			this.OnUp();
			Debug.Log("S");
		}

		public virtual void OnDown() {
			m_animator.SetBool("Press" , true);
		}

		public virtual void OnPress() {

		}

		public virtual void OnUp() {
			m_animator.SetBool("Press" , false);
		}
		
		public void SetControlTarget(Playable arg_playable) {
			m_playable = arg_playable;
		}
	}
}