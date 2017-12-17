using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class UserData {

		/// <summary>ユーザーID</summary>
		public int m_id;

		/// <summary>ユーザーネーム（一応）</summary>
		public string m_name;

		/// <summary>SEの設定音量</summary>
		public float m_seVolume;

		/// <summary>BGMの設定音量</summary>
		public float m_bgmVolume;

		/// <summary>
		/// ユーザー情報の一時保存用
		/// ローカルでは値を保持するが、保存対象には含まれないもの
		/// </summary>
		public class TempData {
			/// <summary>これからプレイするステージの番号</summary>
			public uint m_playStageId;

			/// <summary>これからプレイする小部屋の番号</summary>
			public uint m_playRoomId;
		}

		/// <summary>一時保存データ</summary>
		public TempData m_temp;

		/// <summary>
		/// 保存されたデータを取得する
		/// </summary>
		public void Load() {
			//TODO:NiftyもしくはPlayerPrefsで値の保存を実装する
		}

		/// <summary>
		/// データを保存する
		/// </summary>
		public void Save() {
			//TODO:NiftyもしくはPlayerPrefsで値の保存を実装する
		}
	}
}