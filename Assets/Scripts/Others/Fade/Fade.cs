//担当者：森田　勝
//概要　：フェードの処理を行うクラス
//　　　　フェード処理を開始させる場合は
//　　　　このクラスにアクセスする。
//　　　　実装は、別クラスにて
//参考　：なし

using UnityEngine;
using UnityEngine.UI;

namespace ToyBox.FadeNS {
	public class Fade : MonoBehaviour {

		#region フェードの情報をまとめたクラス
		public class FadeInfo {
			//Fadeに使用するImageコンポーネント
			public Image m_fadeObject;
			//α値の上限
			public const byte MAX = 1;
			//α値の下限
			public const byte ZERO = 0;
			//フェードにかける時間
			public float m_duration;
			//現在のα値
			public float m_currentAlpha;
		}

		private FadeInfo m_fadeInfo;

		#endregion

		//フェードが動いているか
		public bool isActive { get; private set; }

		//フェードのインターフェイス
		private IFadeAction m_fadeAction;

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			m_fadeInfo = new FadeInfo();
			m_fadeInfo.m_fadeObject = GetComponent<Image>();
			isActive = false;
			Clear();
		}

		/// <summary>
		/// フェード開始
		/// </summary>
		/// <param name="arg_fadeAction">実行するフェード</param>
		/// <param name="arg_color">色</param>
		/// <param name="arg_duration">フェードにかける時間</param>
		public void StartFade(IFadeAction arg_fadeAction , Color arg_color , float arg_duration) {
			m_fadeAction = arg_fadeAction;
			m_fadeInfo.m_duration = arg_duration;
			m_fadeInfo.m_fadeObject.color = arg_color;
			m_fadeAction.OnEnter(this.m_fadeInfo);
			isActive = true;
		}

		/// <summary>
		/// アップデート処理
		/// </summary>
		public void UpdateByFrame() {

			if (m_fadeAction.IsEnd(this.m_fadeInfo)) {
				m_fadeAction.OnExit(this.m_fadeInfo);
				isActive = false;

				return;
			}
			m_fadeAction.UpdateByFrame(this.m_fadeInfo);
		}

		/// <summary>
		/// フェードのクリア
		/// </summary>
		public void Clear() {
			m_fadeInfo.m_fadeObject.color = Color.clear;
		}

		/// <summary>
		/// 塗りつぶし
		/// </summary>
		/// <param name="arg_color">色</param>
		public void Fill(Color arg_color) {
			m_fadeInfo.m_fadeObject.color = arg_color;
		}
	}
}