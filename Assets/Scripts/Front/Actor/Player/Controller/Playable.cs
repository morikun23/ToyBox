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
		/// アイテム（ギミック）を取得しようとしたときに
		/// コールする関数
		/// アイテムを掴める状態かチェックする
		/// </summary>
		/// <returns>結果</returns>
		public abstract bool CallWhenWishItem();

		public virtual void LookAtDirection(Direction arg_direction) {
			m_direction = arg_direction;
		}

		/// <summary>
		/// 引数で渡されるアイテムまで手を伸ばして掴む
		/// もし既にアイテムをつかんでいたら手を離します
		/// </summary>
		/// <param name="arg_item"></param>
		public virtual void ReachOutFor(Item arg_item) {
			if (m_playableHand.IsGrasping()) {
				//もし既にものをつかんでいるようであれば放す
				m_playableHand.Release();
				return;
			}
			
			if(arg_item == null) { return; }

			//自身がものをつかめる状況かをチェック
			if (this.CallWhenWishItem()) {
				m_playableHand.SetItemBuffer(arg_item);
				m_playableArm.SetTargetPosition(arg_item.m_transform.position);
				this.m_inputHandle.m_reach = true;
			}
		}
	}
}