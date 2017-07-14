//担当：森田　勝
//概要：アームの動きをインターフェイス化したもの
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public interface IArmAction {
		
		//更新（実装クラスによりアクションが変化する）
		void OnUpdate(Arm arg_arm);

	}
}