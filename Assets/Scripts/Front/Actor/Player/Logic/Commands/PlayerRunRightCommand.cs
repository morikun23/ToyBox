﻿//担当：森田　勝
//概要：プレイヤーを右に移動させるためのコマンドクラス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerRunRightCommand : IPlayerCommand {

		//直前の向き
		private ActorBase.Direction m_directionBuf;

		//直前の座標
		private Vector2 m_positionBuf;

		/// <summary>
		/// コンストラクタ
		/// 実行後、取り消せるようにバッファを用意する
		/// </summary>
		/// <param name="arg_player"></param>
		public PlayerRunRightCommand(Player arg_player) {
			m_directionBuf = arg_player.m_direction;
			m_positionBuf = arg_player.m_transform.position;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="arg_player"></param>
		public void Execute(Player arg_player) {
			arg_player.m_direction = ActorBase.Direction.RIGHT;
			arg_player.m_transform.position += Vector3.right * arg_player.m_speed;
		}

		/// <summary>
		/// 取り消し
		/// </summary>
		/// <param name="arg_player"></param>
		public void Undo(Player arg_player) {
			arg_player.m_direction = m_directionBuf;
			arg_player.m_transform.position = m_positionBuf;
		}
	}
}