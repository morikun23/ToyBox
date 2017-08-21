//担当：森田　勝
//概要：プレイヤーを射出状態に変更させるためのコマンドクラス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerReachCommand : IPlayerCommand {

		//Rigidbody2D
		private Rigidbody2D m_rigidbody;

		//直前の状態
		private IPlayerState m_stateBuf;
		private RigidbodyType2D m_bodyTypeBuf;

		/// <summary>
		/// コンストラクタ
		/// 実行後、取り消せるようにバッファを用意する
		/// </summary>
		/// <param name="arg_player"></param>
		public PlayerReachCommand(Player arg_player) {
			m_stateBuf = arg_player.m_currentState;
			m_rigidbody = arg_player.m_rigidbody;
			m_bodyTypeBuf = arg_player.m_rigidbody.bodyType;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="arg_player"></param>
		public void Execute(Player arg_player) {
			m_rigidbody.bodyType = RigidbodyType2D.Kinematic;
			m_rigidbody.velocity = Vector2.zero;
		}

		/// <summary>
		/// 取り消し
		/// </summary>
		/// <param name="arg_player"></param>
		public void Undo(Player arg_player) {
			m_rigidbody.bodyType = m_bodyTypeBuf;
		}

	}
}