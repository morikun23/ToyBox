//担当：森田　勝
//概要：アームの行動をインターフェイス化したもの
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IArmCommand {

		//実行
		void Execute(Arm arg_arm);

		//取り消し
		void Undo(Arm arg_arm);
	}
}