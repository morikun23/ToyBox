using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ToyBox {

	/// <summary>
	/// ボタンのコールバック（通知）機能
	/// </summary>
	[System.Serializable]
	public class ButtonAction {

		/// <summary>コールバック</summary>
		public System.Action m_action { get; private set; }

		/// <summary>引数ありコールバック</summary>
		public System.Action<object> m_objectAction { get; private set; }

		/// <summary>コールバック時の引数</summary>
		public object m_value;

		/// <summary>
		/// コンストラクタ
		/// コールバックを設定する
		/// </summary>
		/// <param name="arg_action">コールバック</param>
		public ButtonAction(Action arg_action) {
			m_action = arg_action;
		}

		/// <summary>
		/// コンストラクタ
		/// 引数ありコールバックを設定する
		/// </summary>
		/// <param name="arg_objectAction">コールバック</param>
		/// <param name="arg_value">引数</param>
		public ButtonAction(System.Action<object> arg_objectAction , object arg_value) {
			m_objectAction = arg_objectAction;
			m_value = arg_value;
		}
	}

	/// <summary>
	/// ボタンの設定
	/// ボタンが処理をおこなうときの設定がまとまっている
	/// </summary>
	[System.Serializable]
	public class ButtonOption {

		/// <summary>押されたとみなすタイミング</summary>
		[SerializeField]
		private UIButton.EventTrgger m_trigger = UIButton.EventTrgger.OnClick;

		/// <summary>
		/// ボタンが押されたときに自動でスケーリング処理をするか
		/// ここをfalseにすることで独自のアニメーションを実装可能
		/// </summary>
		[SerializeField]
		private bool m_isAutoScale = true;

		/// <summary>
		/// ボタンが押されたとみなすタイミング
		/// </summary>
		public UIButton.EventTrgger Trigger {
			get { return m_trigger; }
		}

		/// <summary>
		/// 自動でスケーリング処理をおこなう
		/// </summary>
		public bool IsAutoScale {
			get { return m_isAutoScale; }
		}
	}

	/// <summary>
	/// UIに使用されるボタンの機能
	/// </summary>
	[AddComponentMenu("ToyBox/UI/Button")]
	public class UIButton : MonoBehaviour,
		IPointerUpHandler,
		IPointerDownHandler,
		IPointerClickHandler {

		/// <summary>ボタンが押されたとみなすタイミング</summary>
		public enum EventTrgger {
			OnClick,
			OnPress,
			OnRelease
		}

		/// <summary>コールバック</summary>
		private ButtonAction m_btnAction;

		/// <summary>ボタンの設定</summary>
		[SerializeField]
		private ButtonOption m_btnOption;

		/// <summary>
		/// 初期化
		/// ここでコールバックを設定する必要がある
		/// </summary>
		/// <param name="arg_action">コールバック</param>
		public void Initialize(System.Action arg_action) {
			if (arg_action == null) {
				Debug.LogError("[ToyBox]数がNULL");
				return;
			}
			m_btnAction = new ButtonAction(arg_action);
		}

		/// <summary>
		/// 初期化
		/// ここでコールバックを設定する必要がある
		/// </summary>
		/// <param name="arg_action">コールバック</param>
		/// <param name="arg_value">コールバック時の引数</param>
		public void Initialize(System.Action<object> arg_action , object arg_value) {
			if (arg_action == null || arg_value == null) {
				Debug.LogError("[ToyBox]引数がNULL");
				return;
			}
			m_btnAction = new ButtonAction(arg_action , arg_value);
		}

		/// <summary>
		/// カーソルが離されたときの処理
		/// </summary>
		/// <param name="eventData"></param>
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
			if (m_btnOption.IsAutoScale) {
				iTween.ScaleTo(this.gameObject , new Vector3(1f , 1f , 1) , 0.5f);
			}

			if (m_btnOption.Trigger != EventTrgger.OnRelease) return;
			Action();
		}

		/// <summary>
		/// カーソルが押されたときの処理
		/// </summary>
		/// <param name="eventData"></param>
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
			if (m_btnOption.IsAutoScale) {
				iTween.ScaleTo(this.gameObject , new Vector3(0.95f , 0.95f , 1) , 0.5f);
			}

			if (m_btnOption.Trigger != EventTrgger.OnPress) return;
			Action();

		}

		/// <summary>
		/// カーソルがクリック動作をおこなったときの処理
		/// </summary>
		/// <param name="eventData"></param>
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
			if (m_btnOption.Trigger != EventTrgger.OnClick) return;
			Action();
		}

		/// <summary>
		/// ボタンが押されたときの処理
		/// </summary>
		private void Action() {

			if (m_btnAction == null) {
				Debug.LogError("[ToyBox]ボタンにアクションが設定されていない");
				return;
			}

			System.Action action = m_btnAction.m_action;

			if (action != null) {
				action();
				return;
			}

			System.Action<object> objectAction = m_btnAction.m_objectAction;
			object value = m_btnAction.m_value;

			if (m_btnAction.m_objectAction != null) {
				m_btnAction.m_objectAction(m_btnAction.m_value);
				return;
			}
		}
	}
}