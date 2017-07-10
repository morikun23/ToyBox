using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Controller {
	public class Player : MonoBehaviour {

		private Logic.Player m_logic { get; set; }
		private View.Player m_view { get; set; }

		private Rigidbody2D m_rigidbody { get; set; }
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {

			m_rigidbody = GetComponent<Rigidbody2D>();
			
			m_logic = new Logic.Player();
			m_view = GetComponent<View.Player>();
			m_logic.Initialize(this);
			m_view.Initialize(m_logic);
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame() {
			if (Input.GetKey(KeyCode.RightArrow)) {
				transform.position += Vector3.right * 0.1f;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				transform.position += Vector3.left * 0.1f;
			}
		}

		/// <summary>
		/// Unity側から衝突の情報を取得する
		/// </summary>
		/// <param name="arg_collsionInfo">衝突した相手の情報</param>
		private void OnCollisionEnter2D(Collision2D arg_collsionInfo) {

		}
	}
}