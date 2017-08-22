using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class Item : ObjectBase {

		public enum State {
			GRABBED,
			PULLEDs,
			CARRIED
		}

		public State m_currentState;

		protected Hand m_owner;

		public void SetOwner(Hand arg_hand) {
			m_owner = arg_hand;
		}

		//
		public virtual void OnGrabbed() {
			//TODO:掴まれたときの処理
			//Armを縮めるのか、固定なのか、自分もついていくのか
			//また、持ち運ばれるのか
		}

		public virtual void OnPulled() {
			//TODO:引っ張られたときの処理
			//持ち運ばれるのか、否か
		}

		/// <summary>
		/// 持ち運びされているときの処理
		/// </summary>
		public virtual void OnCarried() {

		}
		
	}
}