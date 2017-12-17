using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class CheckPoint : MonoBehaviour {

		//チェックポイントを区別するための個別番号
		public int m_id;

		//チェックポイントが機能されているか
		public bool m_isActive { get; protected set; }

		//コールバック
		protected System.Action<object> m_callBack;

		public virtual void Initialize(System.Action<object> arg_callBack) {
			m_callBack = arg_callBack;
		}
		
		protected virtual void OnTriggerEnter2D(Collider2D arg_collider) {

			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
				m_callBack(this);
				m_isActive = true;
			}
		}
	}
}