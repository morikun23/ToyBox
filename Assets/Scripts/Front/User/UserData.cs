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

		/// <summary>現状実装されているステージ数</summary>
		public const float NUM_COMPLETE_STAGE = 2;

		/// <summary>
		/// ユーザー情報の一時保存用
		/// ローカルでは値を保持するが、保存対象には含まれないもの
		/// </summary>
		public class TempData {
			/// <summary>これからプレイするステージの番号</summary>
			public uint m_playStageId;

			/// <summary>これからプレイする小部屋の番号</summary>
			public uint m_playRoomId;

			/// <summary>今遊んでいる小部屋の番号</summary>
			public uint m_playingRoomId;

			/// <summary>各ステージの詳細データ</summary>
			public List<Dictionary<string,object>> m_dic_ = new List<Dictionary<string, object>>();

			/// <summary>選択中ステージの小部屋の詳細データ</summary>
			public List<Dictionary<string,object>> m_dic_room = new List<Dictionary<string, object>>();

			/// <summary>小部屋での経過時間</summary>
			public float m_num_roomWaitTime;

			/// <summary>死亡回数</summary>
			public int m_cnt_death;

			///<summary>PlayerがUIで操作可能かどうか</summary>
			public bool m_isTouchUI = true;

		}

		/// <summary>一時保存データ</summary>
		public TempData m_temp = new TempData();

		/// <summary>
		/// 保存されたデータを取得する
		/// </summary>
		public void Load() {
			m_id = PlayerPrefs.GetInt ("UserId");
		}

		/// <summary>
		/// データを保存する
		/// </summary>
		public void Save() {
			
		}

		/// <summary>
		/// 一時保存データを初期化する
		/// </summary>
		public void DataInitalize(){
			this.m_temp.m_cnt_death = 0;
			this.m_temp.m_playStageId = 0;
			this.m_temp.m_playRoomId = 0;
			this.m_temp.m_num_roomWaitTime = 0;
			this.m_temp.m_dic_room = new List<Dictionary<string, object>>();
		}
	}
}