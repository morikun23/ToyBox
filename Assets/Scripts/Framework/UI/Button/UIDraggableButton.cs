using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ToyBox {
	public class UIDraggableButton : UIButton {

		/// <summary>移動可能範囲を有効化するか</summary>
		[SerializeField,Tooltip("移動可能範囲を有効にするか")]
		protected bool m_isEnableRange = false;

		/// <summary>移動可能範囲(半径)</summary>
		[SerializeField,Tooltip("移動可能範囲")]
		protected float m_radius = 100f;

		/// <summary>初期座標</summary>
		private Vector2 m_defaultPosition;

		/// <summary>初期座標から指へのベクトル</summary>
		private Vector2 m_defaultToFinger;

        /// <summary>
        /// ボタンの最後の更新からの移動方向を取得する
        /// 移動可能範囲が設定されている場合は
        /// 初期位置の移動方向を取得する
        /// </summary>
        public Vector2 Direction {
			get {
				return m_defaultToFinger;
			}
		}

		/// <summary>
		/// 初期位置を取得する
		/// </summary>
		public Vector2 DefaultPosition {
			get {
				return m_defaultPosition;
			}
		}
		
        /// <summary>
        /// 起動時に初期座標を登録する
        /// imageとResources内のSpriteを取得;
        /// </summary>
        protected virtual void Awake() {
			m_defaultPosition = transform.position;
        }

		#region TouchActorからの継承
		protected sealed override void Swipe(PointerEventData data) {

			data.position = Camera.main.ScreenToWorldPoint(data.position);

			if (!m_isEnableRange) {
				transform.position = data.position;
				m_defaultToFinger = data.delta;
				return;
			}

			Vector2 vec = (data.position - m_defaultPosition);
			if (vec.magnitude > m_radius) {
				vec = vec.normalized * m_radius;
			}

			m_defaultToFinger = vec;
			transform.position = m_defaultPosition + m_defaultToFinger;

			base.Swipe(data);
			base.m_isUsing = true;

			this.OnSwipe();

			foreach (var action in m_btnActions.FindAll(_ => _.m_trigger == ButtonEventTrigger.OnSwipe)) {
				base.ExecCallBack(action);
			}
		}
		#endregion

		/// <summary>
		/// カーソルが動いているときの処理
		/// </summary>
		protected virtual void OnSwipe() {
            
		}
    }
}