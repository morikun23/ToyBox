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

			this.Save();
			PlayerPrefs.Save ();

		}

		/// <summary>
		/// IDを発行する
		/// </summary>
		private void CreateUserID(){
			//検索条件を0でない値にセット
			m_NCQ_server.WhereNotEqualTo ("LastUserId", 0);
			//検索し、リスト化して処理実行
			m_NCQ_server.FindAsync ((List<NCMBObject> objList, NCMBException e) => {
				if (e != null) {
					//再接続用モーダルを表示
					UIManager.Instance.PopupNetworkErrorModal(()=>{this.Start();});
					//検索失敗時の処理
				} else {
					//検索成功時、見つかったObjを変数として保持
					m_NCMB_server = objList[0];
					m_NCMB_server.ObjectId = objList[0].ObjectId;

					m_NCMB_server.FetchAsync ((NCMBException f) => {
						if (f == null) {
							//成功時の処理
							//自身をユーザーとして登録
							CreateDataToServer();
							//ローカルにユーザーIdを保存
							CreateDataToLocal();
							m_flg_accept = true;

							Debug.Log("アカウントを作成しました : UserID = " + AppManager.Instance.user.m_id);

							//取得したデータはDictionary型で保持
							AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage1"] as Dictionary<string,object>);
							AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage2"] as Dictionary<string,object>);

						}else if(f != null){
							//再接続用モーダルを表示
							UIManager.Instance.PopupNetworkErrorModal(()=>{this.Start();});
						}
					});

				}
			});

		}

		/// <summary>
		/// IDを検索して取得
		/// </summary>
		void GetUserId(){
			AppManager.Instance.user.m_id = PlayerPrefs.GetInt ("UserId");

			//検索条件をローカルで持っていたユーザーIDにセット
			m_NCQ_.WhereEqualTo ("UserId", AppManager.Instance.user.m_id);
			//検索し、リスト化して処理実行
			m_NCQ_.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
				if(e != null) {
					Debug.Log(e.Message);
					//再接続用モーダルを表示
					UIManager.Instance.PopupNetworkErrorModal(()=>{this.Start();});
				}else if (objList.Count == 0) {
					//検索失敗時の処理
					Debug.LogError("IDがサーバーにみつかりません、不明なユーザーです。\n" +
						"新規ユーザーとして認識します。");
					CreateUserID();
				} else {
					//成功時の処理
					m_NCMB_ = objList[0];
					//取得したデータに置き換え
					m_NCMB_.ObjectId = objList[0].ObjectId;
					m_flg_accept = true;
					Debug.Log("ユーザーデータを読み込みました : UserID = " + AppManager.Instance.user.m_id);

					//取得したデータはDictionary型で保持
					AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage1"] as Dictionary<string,object>);
					AppManager.Instance.user.m_temp.m_dic_.Add(m_NCMB_["data_Stage2"] as Dictionary<string,object>);
						
				}
			});
		}

		/// <summary>
		/// ユーザーデータの初期値をサーバーに保存する
		/// </summary>
		private void CreateDataToServer(){
			
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
			Save();
			m_NCMB_server.SaveAsync ((NCMBException h) => {
				if(h != null){
					//再接続用モーダルを表示
					UIManager.Instance.PopupNetworkErrorModal(()=>{this.Start();});
				}
			});
		}

		/// <summary>
		/// ユーザーデータの初期値をローカルに保存する
		/// </summary>
		private void CreateDataToLocal(){
			PlayerPrefs.SetInt ("UserId", AppManager.Instance.user.m_id);
			PlayerPrefs.Save ();
		}



		/// <summary>
		/// 各データ内容を確定してセーブ
		/// </summary>
		public void Save(){
			if(m_flg_accept){
				m_NCMB_ ["data_Stage1"] = AppManager.Instance.user.m_temp.m_dic_[0];
				m_NCMB_ ["data_Stage2"] = AppManager.Instance.user.m_temp.m_dic_[1];
				m_NCMB_.SaveAsync ((NCMBException e) =>{
					if(e != null){
						//再接続用モーダルを表示
						UIManager.Instance.PopupNetworkErrorModal(()=>{this.Save();});
					}
				});
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
