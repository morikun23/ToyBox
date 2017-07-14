//担当：森田　勝
//概要：プレイヤーを左に移動させるためのクラス
//　　　インターフェイスを実装させたもののため
//　　　右向きとクラスを分けています。
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerRunLeft : IPlayerState {
		
		public void OnEnter(Player arg_player) {
			arg_player.m_direction = CharacterBase.Direction.LEFT;
		}

		public void OnUpdate(Player arg_player) {
			arg_player.m_position += Vector2.left * arg_player.m_speed;
		}

		public void OnExit(Player arg_player) {

		}
		
	}
}