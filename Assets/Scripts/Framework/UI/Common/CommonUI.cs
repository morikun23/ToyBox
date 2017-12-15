using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ToyBox {

	public class CommonUI : MonoBehaviour {

		#region レイヤーの設定

		[Header("Layers")]
		[SerializeField]
		UILayer m_mainLayer;

		[SerializeField]
		UILayer m_frontLayer;

		[SerializeField]
		UILayer m_modalLayer;

		[SerializeField]
		UILayer m_systemLayer;

		#endregion

		protected virtual void Awake() {

#if UNITY_EDITOR
			//NULLチェック
			NullCheck(m_mainLayer);
			NullCheck(m_frontLayer);
			NullCheck(m_modalLayer);
			NullCheck(m_systemLayer);
#endif
		}

#if UNITY_EDITOR
		private void NullCheck(UILayer arg_layer) {
			if(arg_layer == null) {
				Debug.LogError("[ToyBox]" + "<color=red>" + arg_layer.Layer.ToString() + "</color>" + "がNULLです");
			}
		}
#endif
		/// <summary>
		/// メッセージモーダルを表示する
		/// </summary>
		/// <param name="arg_message">表示内容</param>
		/// <param name="arg_callBack">モーダル表示終了後コールバック</param>
		public void PopupMessageModal(string arg_message,System.Action arg_callBack = null) {
			MessageModal modal = Instantiate(ModalContainer.MESSAGE_MODAL ,
				m_modalLayer.AnchorCenter).GetComponent<MessageModal>();

			modal.m_message = arg_message;
			modal.Show(arg_callBack);
		}
		
		/// <summary>
		/// 指定されたレイヤーの入力を有効/無効にする
		/// </summary>
		/// <param name="arg_layerType">レイヤー</param>
		/// <param name="arg_value">有効/無効</param>
		public void SetInputEnable(LayerType arg_layerType,bool arg_value) {
			GameObject layer = null;
			switch (arg_layerType) {
				case LayerType.Main: layer = m_mainLayer.gameObject; break;
				case LayerType.Front: layer = m_frontLayer.gameObject; break;
				case LayerType.Modal: layer = m_modalLayer.gameObject; break;
				case LayerType.System: layer = m_systemLayer.gameObject; break;
			}

			if(layer == null) {
				Debug.LogError("[ToyBox]レイヤーが見つかりません");
				return;
			}

			TouchActor[] touches = layer.GetComponentsInChildren<TouchActor>();
		
			foreach(TouchActor touch in touches) {
				touch.enabled = arg_value;
			}	
		}

		#region CustomEditor
#if UNITY_EDITOR
		/// <summary>
		/// UnityのInspector拡張クラス
		/// </summary>
		[CustomEditor(typeof(CommonUI))]
		public class CommonUIEditor : Editor {

			CommonUI _;

			public override void OnInspectorGUI() {
				base.OnInspectorGUI();

				_ = target as CommonUI;

				if (GUILayout.Button("Find Layers")) {
					this.SetLayer();
				}

				if(GUILayout.Button("Reset Layers")) {
					this.Clearlayer();
				}
			}

			/// <summary>
			/// 子オブジェクトから各レイヤーをセットする
			/// </summary>
			private void SetLayer() {
				UILayer[] layers = _.transform.GetComponentsInChildren<UILayer>();
				_.m_mainLayer = System.Array.Find(layers , __ => __.Layer == LayerType.Main);
				_.m_frontLayer = System.Array.Find(layers , __ => __.Layer == LayerType.Front);
				_.m_modalLayer = System.Array.Find(layers , __ => __.Layer == LayerType.Modal);
				_.m_systemLayer = System.Array.Find(layers , __ => __.Layer == LayerType.System);

				//NULLチェック（一応）
				_.NullCheck(_.m_mainLayer);
				_.NullCheck(_.m_frontLayer);
				_.NullCheck(_.m_modalLayer);
				_.NullCheck(_.m_systemLayer);
			}

			/// <summary>
			/// レイヤーをリセット
			/// </summary>
			private void Clearlayer() {
				_.m_mainLayer = _.m_frontLayer = _.m_modalLayer = _.m_systemLayer = null;
			}
		}
#endif
		#endregion
	}
}