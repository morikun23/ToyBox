//担当：森田　勝
//概要：操作クラスへのインターフェイス
//　　　それぞれ対応するアクションが行われたときに処理させます。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IPlayable {

		void OnLeftButton();

		void OnRightButton();

		void OnJumpButton();

		void OnItemTap(Item arg_item);

	}
}