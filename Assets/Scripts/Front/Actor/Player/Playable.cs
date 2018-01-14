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

		/// <summary>
		/// 自身が腕を伸ばせるか調べる
		/// </summary>
		/// <returns>結果</returns>
		public abstract bool IsAbleReach();
		
	}
}