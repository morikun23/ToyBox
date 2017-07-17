//担当：森田　勝
//概要：プレイヤーを射出状態に変更させるためのコマンドクラス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class PlayerReachCommand : IPlayerCommand {

		//Rigidbody2D
		private Rigidbody2D m_rigidbody;

		//Arm
		private Arm m_arm;
		
		//直前の状態
		private IPlayerState m_stateBuf;
		private RigidbodyType2D m_bodyTypeBuf;
		private Vector2 m_velocityBuf;

		/// <summary>
		/// コンストラクタ
		/// 実行後、取り消せるようにバッファを用意する
		/// </summary>
		/// <param name="arg_player"></param>
		public PlayerReachCommand(Player arg_player,Rigidbody2D arg_rigidbody,Arm arg_arm) {
			m_stateBuf = arg_player.m_currentState;
			m_rigidbody = arg_rigidbody;
			m_bodyTypeBuf = arg_rigidbody.bodyType;
			m_velocityBuf = arg_rigidbody.velocity;
			m_arm = arg_arm;
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
			arg_player.StateTransition(m_stateBuf);
			m_rigidbody.bodyType = m_bodyTypeBuf;
			m_rigidbody.velocity = m_velocityBuf;
		}

	}
}