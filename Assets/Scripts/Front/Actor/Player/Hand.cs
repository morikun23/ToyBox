using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {

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
		
		/// <summary>物をつかんでいる状態である</summary>
		private bool m_isGrasping;

		/// <summary>現在掴んでいるアイテム</summary>
		private Item m_grapingItem;
		
		/// <summary>コールバックを受け取る対象</summary>
		private readonly List<IHandCallBackReceiver> m_callBackReceivers = new List<IHandCallBackReceiver>();

		/// <summary>
		/// 現在掴んでいるアイテム
		/// ※読み取り専用
		/// </summary>
		public Item GraspingItem {
			get {
				return m_grapingItem;
			}
		}

		/// <summary>
		/// アイテムをつかんでいるか
		/// ※読み取り専用
		/// </summary>
		public bool IsGrasping {
			get {
				return m_isGrasping;
			}
		}

		/// <summary>
		/// OnEnable by Unity
		/// 不具合防止のため
		/// 有効化時に初期化を行う
		/// </summary>
		private void OnEnable() {
			m_grapingItem = null;
			m_isGrasping = false;
		}

		/// <summary>
		/// 指定されたアイテムを掴む
		/// </summary>
		/// <param name="arg_item">掴む対象</param>
		private void Grasp(Item arg_item) {
			if (arg_item == null) return;
			m_isGrasping = true;
			m_grapingItem = arg_item;
			foreach (IHandCallBackReceiver receiver in m_callBackReceivers) {
				receiver.OnGrasped(this);
			}
		}

		/// <summary>
		/// 掴んでいるものを放す
		/// </summary>
		public void Release() {
			if (!m_isGrasping) return;
			m_isGrasping = false;
			foreach (IHandCallBackReceiver receiver in m_callBackReceivers) {
				receiver.OnReleased(this);
			}
			m_grapingItem = null;
		}

		/// <summary>
		/// 壁に衝突
		/// </summary>
		private void Collided() {
			foreach (IHandCallBackReceiver receiver in m_callBackReceivers) {
				receiver.OnCollided(this);
			}
		}

		/// <summary>
		/// コールバックを受け取る対象を追加する
		/// </summary>
		/// <param name="arg_callBackReceiver"></param>
		public void AddCallBackReceiver(IHandCallBackReceiver arg_callBackReceiver) {
			m_callBackReceivers.Add(arg_callBackReceiver);
		}

		/// <summary>
		/// CallBack by Unity
		/// </summary>
		/// <param name="arg_collider"></param>
		private void OnTriggerEnter2D(Collider2D arg_collider) {
			if (arg_collider.gameObject.layer == LayerMask.NameToLayer("Item")) {
				if (!m_isGrasping) {
					Item item = arg_collider.GetComponent<Item>();
					if(item.IsAbleGrasp()) this.Grasp(item);
				}
			}
			else if(arg_collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				this.Collided();
			}
		}

	}
}