﻿using System.Collections;
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
	}
}