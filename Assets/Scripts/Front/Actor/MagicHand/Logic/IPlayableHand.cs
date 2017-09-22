//担当

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public interface IPlayableHand {

		/// <summary>
		/// タップしたアイテムをバッファとして持たせる
		/// 伸ばす前にこれを実行してください
		/// </summary>
		/// <param name="arg_item"></param>
		void SetItemBuffer(Item arg_item);

		/// <summary>
		/// 所持しているアイテムを離す
		/// </summary>
		void Release();

		/// <summary>
		/// 現在アイテムを掴んでいる状態か調べる
		/// </summary>
		/// <returns></returns>
		bool IsGrasping();
	}
}