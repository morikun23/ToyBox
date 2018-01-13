using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {

	/// <summary>
	/// ハンドからコールバックを受け取るためのインターフェイス
	/// </summary>
	public interface IHandCallBackReceiver {

		/// <summary>
		/// ハンドが壁などに衝突したときに実行される
		/// </summary>
		void OnCollided(Hand arg_hand);

		/// <summary>
		/// ハンドが物をつかんだときに実行される
		/// </summary>
		void OnGrasped(Hand arg_hand);

		/// <summary>
		/// ハンドが物を放したときに実行される
		/// </summary>
		void OnReleased(Hand arg_hand);
	}

	public class Hand : MonoBehaviour {

		private bool m_isGrasping;

		private Item m_grapingItem;

		private IHandCallBackReceiver m_callBackReceiver;

		/// <summary>
		/// 初期化/起動メソッド
		/// </summary>
		/// <param name="arg_callBackReceiver">コールバックを受け取る対象</param>
		public void Initialize(IHandCallBackReceiver arg_callBackReceiver) {
			m_callBackReceiver = arg_callBackReceiver;

			//デフォルトで非表示
			this.gameObject.SetActive(false);
			
		}

		/// <summary>
		/// 指定されたアイテムを掴む
		/// </summary>
		/// <param name="arg_item">掴む対象</param>
		private void Grasp(Item arg_item) {
			if (arg_item == null) return;
			m_isGrasping = true;
			m_grapingItem = arg_item;
			m_callBackReceiver.OnGrasped(this);
		}

		/// <summary>
		/// 掴んでいるものを放す
		/// </summary>
		public void Release() {
			if (!m_isGrasping) return;
			m_isGrasping = false;
			m_grapingItem = null;
			m_callBackReceiver.OnReleased(this);
		}

		/// <summary>
		/// 壁に衝突
		/// </summary>
		private void Collided() {
			m_callBackReceiver.OnCollided(this);
		}
		
		private IEnumerator OnGraspStay() {
			while (true) {

				yield return null;
			}
		}

		/// <summary>
		/// CallBack by Unity
		/// </summary>
		/// <param name="arg_collider"></param>
		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Item")) {
				if (!m_isGrasping) {
					this.Grasp(arg_collider.GetComponent<Item>());
				}
			}

			if(arg_collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				this.Collided();
			}
		}

	}
}