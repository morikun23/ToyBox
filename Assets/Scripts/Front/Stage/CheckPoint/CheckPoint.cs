using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class CheckPoint : MonoBehaviour {

		//チェックポイントを区別するための個別番号
		public int m_id;

		//チェックポイントが機能されているか
		public bool m_isActive { get; protected set; }

		//コールバック
		protected System.Action<int> CallBack;

		public virtual void Initialize(System.Action<int> arg_callBack) {
			CallBack = arg_callBack;
		}
		
		protected virtual void OnTriggerEnter2D(Collider2D arg_collider) {

			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
				CallBack(m_id);
				m_isActive = true;
			}
		}
	}
}