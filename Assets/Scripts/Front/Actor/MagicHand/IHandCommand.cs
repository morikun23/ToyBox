//担当：森田　勝
//概要：ハンド部分の行動をインターフェイス化したもの
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IHandCommand {

		//実行
		void Execute(Hand arg_hand);

		//取り消し
		void Undo(Hand arg_hand);
	}
}