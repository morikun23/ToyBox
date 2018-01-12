using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {

	public interface IHandCallBackReceiver {

		/// <summary>
		/// ハンドが壁などに衝突したときに実行される
		/// </summary>
		void OnCollided();

		/// <summary>
		/// ハンドが物をつかんだときに実行される
		/// </summary>
		void OnGrasped();

		/// <summary>
		/// ハンドが物を放したときに実行される
		/// </summary>
		void OnReleased();
	}


	public class Hand : MonoBehaviour {

		private bool m_isGrasping;

		private Item m_grapingItem;

		private IHandCallBackReceiver m_callBackReceiver;

		public void Initialize(IHandCallBackReceiver arg_callBackReceiver) {
			m_callBackReceiver = arg_callBackReceiver;
		}

		private void Grasp(Item arg_item) {
			if (arg_item == null) return;
			m_grapingItem = arg_item;
			m_callBackReceiver.OnGrasped();
		}

		private void Collided() {
			m_callBackReceiver.OnCollided();
		}

		public void Release() {
			m_grapingItem = null;
			m_callBackReceiver.OnReleased();
		}

		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Item")) {
				this.Grasp(arg_collider.GetComponent<Item>());
			}

			if(arg_collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				this.Collided();
			}
		}
	}
}