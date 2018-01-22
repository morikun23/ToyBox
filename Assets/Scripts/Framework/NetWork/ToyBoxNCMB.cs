//担当者：久野直之
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


		//ユーザーデータ検索用
		NCMBQuery<NCMBObject> m_NCQ_ = new NCMBQuery<NCMBObject>("UserData");
		//サーバーデータ格納用
		NCMBObject m_NCMB_server = new NCMBObject("ServerData");
		//サーバーデータ検索用
		NCMBQuery<NCMBObject> m_NCQ_server = new NCMBQuery<NCMBObject>("ServerData");

		//初期化
		public void Start () {

			//ローカルからPlayerIDを抽出できない場合、サーバーと接続してIDの新規発行
			if (!PlayerPrefs.HasKey ("UserId")) {
				Debug.Log ("IDがローカルに見つかりません、新規作成します。");
				CreateUserID();
			} else {
				GetUserId ();
				Debug.Log ("IDがローカルで見つかりました。サーバーと接続…");
			}

			m_NCMB_.SaveAsync ();
			PlayerPrefs.Save ();

		}

		/// <summary>
		/// IDを発行する
		/// </summary>
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
								AppManager.Instance.user.m_id = System.Convert.ToInt32 (m_NCMB_server ["LastUserId"]);
								AppManager.Instance.user.m_id += 1;

								//ユーザーデータ
								m_NCMB_server ["LastUserId"] = AppManager.Instance.user.m_id;
								m_NCMB_ ["UserId"] = AppManager.Instance.user.m_id;
								//初期化
								Dictionary<string,object> dic = new Dictionary<string,object>();
								ArrayList list = new ArrayList();
								list.Add(0);
								dic["GoalTime"] = list;
								m_NCMB_ ["data_Stage1"] = dic;
								dic = new Dictionary<string, object>();
								list = new ArrayList();
								list.Add(0);
								dic["GoalTime"] = list;
								m_NCMB_ ["data_Stage2"] = dic;


								//現状をセーブ
								m_NCMB_.SaveAsync ();
								m_NCMB_server.SaveAsync ();

								//ローカルにユーザーIdを保存
								PlayerPrefs.SetInt ("UserId", AppManager.Instance.user.m_id);
								PlayerPrefs.Save ();

								m_num_id = AppManager.Instance.user.m_id;

								m_flg_accept = true;

								Debug.Log("アカウントを作成しました");

								//取得したデータはDictionary型で保持
								AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage1"] as Dictionary<string,object>);
								AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage2"] as Dictionary<string,object>);

							}
						});
					}
				}
			});

		}

		/// <summary>
		/// IDを検索して取得
		/// </summary>
		void GetUserId(){
			m_num_id = PlayerPrefs.GetInt ("UserId");

			//検索条件をローカルで持っていたユーザーIDにセット
			m_NCQ_.WhereEqualTo ("UserId", AppManager.Instance.user.m_id);
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
								AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage1"] as Dictionary<string,object>);
								AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage2"] as Dictionary<string,object>);
							}
						});
					}
				}
			});
		}

		/// <summary>
		/// 各データ内容を確定してセーブ
		/// </summary>
		public void Save(){
			if(m_flg_accept){
				m_NCMB_ ["data_Stage1"] = AppManager.Instance.user.m_temp.m_dic_[0];
				m_NCMB_ ["data_Stage2"] = AppManager.Instance.user.m_temp.m_dic_[1];
				m_NCMB_.SaveAsync ();
			}
		}

		/// <summary>
		/// データが書き込める状況かどうかを返す
		/// </summary>
		public bool IsAbleNCMBWrrite(){
			return m_flg_accept;
		}

	}


}
