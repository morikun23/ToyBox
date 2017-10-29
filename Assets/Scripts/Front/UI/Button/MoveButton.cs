using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class MoveButton : Button {

		[SerializeField]
		private ActorBase.Direction m_direction;

		int m_cnt_pushBarrage = 0;
		int m_cnt_live = 0;

		public override void Initialize() {
			base.Initialize();
		}

		public override void UpdateByFrame() {

			m_cnt_live++;
			if(m_cnt_pushBarrage > 0){
				if (m_cnt_live % 15 == 0)
					m_cnt_pushBarrage -= 1;
			}

			base.UpdateByFrame();

#if UNITY_EDITOR && DEVELOP
			if (Input.GetKeyDown(m_key)) {
				this.OnDown();
				return;
			}
			if (Input.GetKey(m_key)) {
				this.OnPress();
			}
			if (Input.GetKeyUp(m_key)) {
				this.OnUp();
			}
#endif
		}

		public override void OnDown() {
			base.OnDown();

			Debug.Log (m_cnt_pushBarrage);
			m_cnt_pushBarrage += 1;
			if(m_cnt_pushBarrage > 10){
				PlayerComponent hoge = m_playable as PlayerComponent;
				hoge.Dead ();
				m_cnt_pushBarrage = 0;
			}
		}

		public override void OnPress() {
			base.OnPress();
			m_playable.m_direction = m_direction;
			m_playable.m_inputHandle.m_run = true;

		}

		public override void OnUp() {
			base.OnUp();
			m_playable.m_inputHandle.m_run = false;
		}

	}
}