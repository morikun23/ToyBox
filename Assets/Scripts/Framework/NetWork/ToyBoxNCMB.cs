﻿//担当者：久野直之
//概要：NCMBのサーバーとローカルでユーザー認証を行い、一致したデータを保持するクラス。
//参考：http://mb.cloud.nifty.com/doc/current/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

namespace ToyBox{
	public class ToyBoxNCMB : MonoBehaviour {

		#region singleton実装
		private static ToyBoxNCMB m_instance;

		public static ToyBoxNCMB Instance {
			get {
				if(m_instance == null) {
					m_instance = FindObjectOfType<ToyBoxNCMB>();
				}
				return m_instance;
			}
		}
		#endregion

		//情報の送受信が完了しているか？
		public static bool m_flg_accept;

		//プレーヤーの現行ID
		public int m_num_id;

		//ユーザーデータ格納用
		public NCMBObject m_NCMB_ = new NCMBObject("UserData");
		public Dictionary<string,object> m_dic = new Dictionary<string,object>();

		//ユーザーデータ検索用
		NCMBQuery<NCMBObject> m_NCQ_ = new NCMBQuery<NCMBObject>("UserData");
		//サーバーデータ格納用
		NCMBObject m_NCMB_server = new NCMBObject("ServerData");
		//サーバーデータ検索用
		NCMBQuery<NCMBObject> m_NCQ_server = new NCMBQuery<NCMBObject>("ServerData");

		// Use this for initialization
		void Start () {

			//ローカルからPlayerIDを抽出できない場合、サーバーと接続してIDの新規発行
			if (!PlayerPrefs.HasKey ("UserId")) {
				CreateUserID();
				Debug.Log ("IDがローカルに見つかりません、新規作成します。");
			} else {
				GetUserId ();
				Debug.Log ("IDがローカルで見つかりました。サーバーと接続…");
			}

			m_NCMB_.SaveAsync ();
			PlayerPrefs.Save ();

		}

		//ID発行
		void CreateUserID(){

			//検索条件を0でない値にセット
			m_NCQ_server.WhereNotEqualTo ("LastUserId", 0);

			//検索し、リスト化して処理実行
			m_NCQ_server.FindAsync ((List<NCMBObject> objList, NCMBException e) => {
				if (e != null) {
					//検索失敗時の処理
				} else {
					//検索成功時、見つかったObjを変数として保持
					foreach (NCMBObject obj in objList) {
						m_NCMB_server = obj;
						m_NCMB_server.ObjectId = obj.ObjectId;

						m_NCMB_server.FetchAsync ((NCMBException f) => {        
							if (f == null) {
								//成功時の処理
								//自身をユーザーとして登録
								m_num_id = System.Convert.ToInt32 (m_NCMB_server ["LastUserId"]);
								m_num_id += 1;

								//ユーザーデータ
								m_NCMB_ ["UserId"] = m_num_id;
								m_NCMB_server ["LastUserId"] = m_num_id;

								//現状をセーブ
								m_NCMB_.SaveAsync ();
								m_NCMB_server.SaveAsync ();

								//ローカルにユーザーIdを保存
								PlayerPrefs.SetInt ("UserId", m_num_id);
								PlayerPrefs.Save ();

								m_flg_accept = true;
							}               
						});
					}
				}
			});

		}

		//IDを検索して取得
		void GetUserId(){
			m_num_id = PlayerPrefs.GetInt ("UserId");

			//検索条件をローカルで持っていたユーザーIDにセット
			m_NCQ_.WhereEqualTo ("UserId", m_num_id);
			//検索し、リスト化して処理実行
			m_NCQ_.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
				if (objList.Count == 0) {
					//検索失敗時の処理
					Debug.Log("IDがサーバーにみつかりません、不明なユーザーです");
					Debug.Log("新規ユーザーとして認識します。");
					CreateUserID();
				} else {
					//検索成功時、見つかったObjを変数として保持
					foreach (NCMBObject obj in objList) {
						//オブジェクトの新規作成をなかったことにして、代わりにサーバーからデータを取得
						m_NCMB_.DeleteAsync((NCMBException g) =>{
							if(g == null){
								m_NCMB_ = obj;
								//取得したデータに置き換え
								m_NCMB_.ObjectId = obj.ObjectId;
								m_flg_accept = true;
								Debug.Log("ユーザーデータを読み込みました");

								//取得したデータはDictionary型で保持
								m_dic = m_NCMB_["ToyBoxData"] as Dictionary<string,object>;

							}
						});
					}
				}
			});
		}

		//各データ内容を確定してセーブ
		public void Save(){
			if(m_flg_accept){
				m_NCMB_ ["ToyBoxData"] = m_dic;
				m_NCMB_.SaveAsync ();
			}
		}

		//データが書き込める状況かどうかを返す
		public bool IsAbleNCMBWrrite(){
			return m_flg_accept;
		}

	}


}
