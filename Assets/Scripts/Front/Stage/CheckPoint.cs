using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class CheckPoint : MonoBehaviour {

		//チェックポイントを区別するための個別番号
		public int m_id;

		//チェックポイントが機能されているか
		public bool m_isActive { get; protected set; }

		//初期リスタート地点であるか
		[SerializeField]
		private bool m_isInitPosition;

		//コールバック
		System.Action<int> CallBack;

		public void Initialize(System.Action<int> arg_callBack) {
			CallBack = arg_callBack;
			if (m_isInitPosition) { m_isActive = true; }
		}
		
		protected virtual void OnTriggerEnter2D(Collider2D arg_collider) {
			if(!m_isActive){
				AudioSource source = AppManager.Instance.m_audioManager.CreateSe ("SE_SavePoint");
				source.Play ();
			}

			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
				CallBack(m_id);
				m_isActive = true;
			}
		}
	}
}