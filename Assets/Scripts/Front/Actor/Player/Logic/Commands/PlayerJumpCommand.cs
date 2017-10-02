//担当：森田　勝
//概要：プレイヤーをジャンプさせるためのコマンドクラス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayerJumpCommand : IPlayerCommand {

		private Rigidbody2D m_rigidbody;
		private Vector2 m_velocityBuf;
		private Vector2 m_positionBuf;

		private const float JUMP_POWER = 250f;

		/// <summary>
		/// コンストラクタ
		/// 必要なコンポーネントを参照する
		/// </summary>
		/// <param name="arg_rigidBody"></param>
		public PlayerJumpCommand(Player arg_player) {
			m_rigidbody = arg_player.m_rigidbody;
			m_velocityBuf = arg_player.m_rigidbody.velocity;
			m_positionBuf = arg_player.m_transform.position;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="arg_player"></param>
		public void Execute(Player arg_player) {
			m_rigidbody.AddForce(Vector2.up * JUMP_POWER);
		}

		/// <summary>
		/// 取り消し
		/// </summary>
		/// <param name="arg_player"></param>
		public void Undo(Player arg_player) {
			arg_player.m_transform.position = m_positionBuf;
			m_rigidbody.velocity = m_velocityBuf;
		}
		
	}
}