using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ToyBox {

	/// <summary>
	/// レイヤーの種類
	/// 数字が大きいものほど前に表示させる
	/// </summary>
	public enum LayerType {
		Main = 0,
		Front = 1,
		Modal = 2,
		System = 3
	}

	[AddComponentMenu("ToyBox/UI/Layer")]
	public class UILayer : MonoBehaviour {

		[SerializeField,Tooltip("レイヤーの種類")]
		private LayerType m_layerType;

		[Header("Anchors")]
		[SerializeField,Tooltip("左固定アンカー")]
		private RectTransform m_anchorLeft;

		[SerializeField,Tooltip("中央固定アンカー")]
		private RectTransform m_anchorCenter;

		[SerializeField,Tooltip("右固定アンカー")]
		private RectTransform m_anchorRight;

		/// <summary>
		/// レイヤーの種類
		/// ※読み取り専用
		/// </summary>
		public LayerType Layer {
			get {
				return m_layerType;
			}
		}
		
		/// <summary>
		/// 左固定アンカー
		/// ※読み取り専用
		/// </summary>
		public RectTransform AnchorLeft {
			get {
				return m_anchorLeft;
			}
		}

		/// <summary>
		/// 中央固定アンカー
		/// ※読み取り専用
		/// </summary>
		public RectTransform AnchorCenter {
			get {
				return m_anchorCenter;
			}
		}

		/// <summary>
		/// 右固定アンカー
		/// ※読み取り専用
		/// </summary>
		public RectTransform AnchorRight {
			get {
				return m_anchorRight;
			}
		}

		#region NULLチェック
		protected void Awake() {
			this.NullCheck();	
		}

		private void NullCheck() {
			if (AnchorLeft == null) {
				Debug.LogError("[ToyBox]" + "<color=red>" + Layer.ToString() + "</color>"
					+ "レイヤーの<color=red>AnchorLeft</color>が設定されていません");
			}
			if (AnchorCenter == null) {
				Debug.LogError("[ToyBox]" + "<color=red>" + Layer.ToString() + "</color>"
					+ "レイヤーの<color=red>AnchorCenter</color>が設定されていません");
			}
			if (AnchorRight == null) {
				Debug.LogError("[ToyBox]" + "<color=red>" + Layer.ToString() + "</color>"
					+ "レイヤーの<color=red>AnchorRight</color>が設定されていません");
			}
		}

		#endregion

		#region CustomEditor
#if UNITY_EDITOR
		[CustomEditor(typeof(UILayer))]
		private class UILayerEditor : Editor{

			private UILayer _;

			private const string ANCHOR_LEFT = "AnchorLeft";
			private const string ANCHOR_CENTER = "AnchorCenter";
			private const string ANCHOR_RIGHT = "AnchorRight";

			public override void OnInspectorGUI() {
				base.OnInspectorGUI();

				_ = target as UILayer;

				if (GUILayout.Button("Find Anchors")) {
					this.FindAnchors();
				}

				if (GUILayout.Button("Reset Anchors")) {
					this.ResetAnchors();
				}
			}

			/// <summary>
			/// 子オブジェクトからアンカーを探す
			/// </summary>
			private void FindAnchors() {
				_.m_anchorLeft = _.transform.Find(ANCHOR_LEFT) as RectTransform;
				_.m_anchorCenter = _.transform.Find(ANCHOR_CENTER) as RectTransform;
				_.m_anchorRight = _.transform.Find(ANCHOR_RIGHT) as RectTransform;
				_.NullCheck();
			}

			/// <summary>
			/// アンカーをリセット
			/// </summary>
			private void ResetAnchors() {
				_.m_anchorLeft = _.m_anchorCenter = _.m_anchorRight = null;
			}
		}
#endif
		#endregion
	}
}