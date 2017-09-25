//担当：森田　勝
//概要：マジックハンドの部分を制御する
//　　　マジックハンドの仕事は、掴む、握る、放すの動作など
//参考：とくになし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class HandComponent : ObjectBase, IPlayableHand {

		public bool m_IsGrasping { get; protected set; }

		protected HandViewer m_viewer;

		public Item m_itemBuffer { get; protected set; }

		public Item m_graspingItem;

		public PlayerComponent m_owner { get; protected set; }

		public void SetItemBuffer(Item arg_item) {
			m_itemBuffer = arg_item;
		}

		public void CallResultArmLengthend(bool arg_result) {
#if DEVELOP
			Debug.Log("CallLengthend: " + arg_result);
#endif
			if (arg_result) {
				m_graspingItem = m_itemBuffer;
				m_IsGrasping = true;
			}
			m_itemBuffer = null;
		}

		public void Release() {
			if (m_graspingItem) {
				m_graspingItem.OnGraspedExit(m_owner);
				m_graspingItem = null;
			}
			m_itemBuffer = null;
			m_IsGrasping = false;
		}

		public void SetOwner(PlayerComponent arg_player) {
			m_owner = arg_player;
		}

		public bool IsGrasping() {
			return m_IsGrasping;
		}

	}
}