using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class RoomCollider : MonoBehaviour {

		[Header("部屋番号")]
		[SerializeField]
		private int m_NextPlayRoom, m_PrevPlayRoom;

		//コールバック
		private System.Action<int,int> CallBack;

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_action">通知用関数インスタンス</param>
		public void Initialize(System.Action<int,int> arg_callBack) {
			CallBack = arg_callBack;
		}

		/// <summary>
		/// Stageへ通知を送る
		/// </summary>
		/// <param name="arg_col"></param>
		void OnTriggerEnter2D(Collider2D arg_col) {
			if (arg_col.gameObject.layer == LayerMask.NameToLayer("Player")) {
				CallBack(m_PrevPlayRoom , m_NextPlayRoom);
			}
		}

	}

}