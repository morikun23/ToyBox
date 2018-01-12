using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class Playable : ActorBase {

		[System.Serializable]
		public class InputHandle {
			public bool m_run;
			public bool m_jump;
			public bool m_reach;
            public bool m_ableRun = true;
            public bool m_ableJump = true;
            public bool m_ableReach = true;
		}

		//プレイヤーへの入力ハンドラ
		public InputHandle m_inputHandle;

		//アームへの操作インターフェイス
		public PlayableArm m_playableArm { get; protected set; }

		//ハンドへの操作インターフェイス
		public IPlayableHand m_playableHand { get; protected set; }

		//地面に接しているか
		public bool m_isGrounded { get; protected set; }

		/// <summary>
		/// 自身が腕を伸ばせるか調べる
		/// </summary>
		/// <returns>結果</returns>
		public abstract bool IsAbleReach();

		public virtual void LookAtDirection(Direction arg_direction) {
			m_direction = arg_direction;
		}

		/// <summary>
		/// 引数で渡されるアイテムまで手を伸ばして掴む
		/// </summary>
		/// <param name="arg_item"></param>
		public virtual void ReachOutFor(Item arg_item) {
			if (arg_item) {
				if (arg_item.IsAbleGrasp() && this.IsAbleReach()) {
					m_playableHand.SetItemBuffer(arg_item);
					m_playableArm.SetTargetPosition(arg_item.transform.position);
					this.m_inputHandle.m_reach = true;
				}
			}
		}

		/// <summary>
		/// 持っているものを放す
		/// </summary>
		public virtual void Release() {
			if (m_playableHand.GraspingItem != null) {
				if (m_playableHand.GraspingItem.IsAbleRelease()) {
					m_playableHand.Release();
				}
			}
		}
	}
}