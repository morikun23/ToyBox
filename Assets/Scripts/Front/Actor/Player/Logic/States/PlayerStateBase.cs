//担当：森田　勝
//概要：プレイヤーのステートを実装するにあたって
//　　　共通部分をここにまとめた。
//　　　ステートの遷移ができるかなどの処理はここでおこなう
//　　　実際にステートを実装するときは、遷移できないステートだけ
//　　　関数をオーバーライドしてfalseを返してください
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class PlayerStateBase {

		protected virtual bool AbleRun{ get { return true; } }

		protected virtual bool AbleJump{ get { return true; } }

		protected virtual bool AbleReach{ get { return true; } }

	}
}