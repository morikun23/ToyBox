using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {

	/// <summary>
	/// レイヤーの種類
	/// 数字が大きいものほど前に表示させる
	/// </summary>
	public enum UILayer : int {
		DEFAULT = 0,
		MAIN = 1,
		FRONT = 2,
		MODAL = 3,
		SYSTEM = 4
	}

	public class UIManager : MonoBehaviour {

		private static UIManager m_instance;

		public static UIManager Instance {
			get {
				if(m_instance == null) {
					m_instance = FindObjectOfType<UIManager>();
				}
				return m_instance;
			}
		}

		private Camera m_uiCamera;

		private Camera UICamera {
			get {
				if(m_uiCamera == null) {
					m_uiCamera = System.Array.Find(FindObjectsOfType<Camera>() ,
						_ => _.name == "UICamera");
					if(m_uiCamera == null) {
						Debug.LogError("[ToyBox]UICameraが見つかりません");
					}
				}
				return m_uiCamera;
			}
		}


		private void Awake() {
			if(Instance != this) {
				Destroy(this.gameObject);
			}
		}

		/// <summary>
		/// メッセージモーダルを表示する
		/// </summary>
		/// <param name="arg_message">表示内容</param>
		/// <param name="arg_callBack">モーダル表示終了後コールバック</param>
		public void PopupMessageModal(string arg_message , System.Action arg_callBack = null) {

			MessageModal modal = InstantiateModal(ModalContainer.MESSAGE_MODAL) as MessageModal;

			modal.m_message = arg_message;
			modal.Show(arg_callBack);
			
		}

		private ModalController InstantiateModal(GameObject arg_prefab) {
			Canvas canvas = Instantiate(arg_prefab).GetComponent<Canvas>();

			canvas.renderMode = RenderMode.ScreenSpaceCamera;
			canvas.worldCamera = UICamera;
			return canvas.GetComponentInChildren<ModalController>();
		}

	}
}