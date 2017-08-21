//担当：森田　勝
//概要：プレイヤーの行動を抽象化させたインターフェイス。
//　　　名前の通りCommandパターンで実装している
//参考：ゼミ内参考書「GameProgrammingPatterns」

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IPlayerCommand {

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="arg_player">実行するプレイヤー</param>
		void Execute(Player arg_player);

		/// <summary>
		/// 実行後に取り消す場合に使用する
		/// 直前の状態に戻す
		/// </summary>
		/// <param name="arg_player">実行するプレイヤー</param>
		void Undo(Player arg_player);
	}
}