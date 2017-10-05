//担当：森田　勝
//概要：マジックハンドの部分を制御する
//　　　マジックハンドの仕事は、掴む、握る、放すの動作など
//参考：とくになし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class HandComponent : ObjectBase, IPlayableHand {

		//現在、掴んでいる状態か
		protected bool m_IsGrasping;

		//描画コンポーネント
		protected HandViewer m_viewer;

		/// <summary>
		/// これから掴むものをバッファとして保持ためのもの
		/// このバッファが参照できなくたったら例外処理を行わせる
		/// </summary>
		public Item m_itemBuffer { get; protected set; }

		/// <summary>
		/// 現在掴んでいるもの
		/// ※バッファと違って目に見えて掴んでいるとき
		/// </summary>
		public Item m_graspingItem { get; protected set; }

#if DEVELOP
		[SerializeField]
		protected Item dev_graspingItem;

		[SerializeField]
		protected bool dev_isGrasping;
#endif

		//持ち主
		public PlayerComponent m_owner { get; protected set; }

		/// <summary>
		/// バッファを設定する
		/// </summary>
		/// <param name="arg_item"></param>
		public void SetItemBuffer(Item arg_item) {
			m_itemBuffer = arg_item;
		}

		/// <summary>
		/// アームが伸ばし切ることを成功するかコールバックさせる
		/// </summary>
		/// <param name="arg_result"></param>
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

		/// <summary>
		/// 掴んでいるものを放す
		/// </summary>
		public void Release() {
			if (m_graspingItem) {
				m_graspingItem.OnGraspedExit(m_owner);
				m_graspingItem = null;
			}
			m_itemBuffer = null;
			m_IsGrasping = false;
		}

		/// <summary>
		/// 現在、掴んでいる状態か
		/// </summary>
		/// <returns></returns>
		public bool IsGrasping() {
			return m_IsGrasping;
		}

		/// <summary>
		/// 自身の持ち主を設定する
		/// </summary>
		/// <param name="arg_player"></param>
		public void SetOwner(PlayerComponent arg_player) {
			m_owner = arg_player;
		}

	}
}