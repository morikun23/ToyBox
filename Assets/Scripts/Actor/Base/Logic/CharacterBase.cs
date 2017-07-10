//担当：森田　勝
//概要：ゲーム内のキャラクターの基底クラス
//　　　ここでのキャラクターとはステージ内を「移動」して
//　　　「向き」という概念のあるものとします。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public abstract class CharacterBase : ActorBase {

		//移動速度
		public float m_speed;

		//向き
		public enum Direction {
			RIGHT = 1,
			LEFT = -1
		}

		//現在の向き
		public Direction m_direction;

		protected CharacterBase() { }
	}
}