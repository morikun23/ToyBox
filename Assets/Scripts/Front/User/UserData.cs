﻿using System.Collections;
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

			//各ステージの詳細データ
			public Dictionary<string,object> m_dic_stage1 = new Dictionary<string,object>();
			public Dictionary<string,object> m_dic_stage2 = new Dictionary<string,object>();
		
			//死亡回数
			public int m_cnt_death;
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
		}
	}
}