//担当者：久野直之
//概要：NCMBのサーバーとローカルでユーザー認証を行い、一致したデータを保持するクラス。
//　保持したデータはstatic属性で所持しているので、外部から自由に編集が可能です。
//参考：
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

namespace ToyBox{
	public class ToyBoxNCMB : MonoBehaviour {

		//情報の送受信が完了しているか？
		public static bool flg_accept;

		//プレーヤーの現行ID
		public int num_id;

		//ユーザーデータ格納用
		public static NCMBObject NCMB_ = new NCMBObject("UserData");
		//ユーザーデータ検索用
		NCMBQuery<NCMBObject> NCQ_ = new NCMBQuery<NCMBObject>("UserData");
		//サーバーデータ格納用
		NCMBObject NCMB_server = new NCMBObject("ServerData");
		//サーバーデータ検索用
		NCMBQuery<NCMBObject> NCQ_server = new NCMBQuery<NCMBObject>("ServerData");

		// Use this for initialization
		void Start () {

			//ローカルからPlayerIDを抽出できない場合、サーバーと接続してIDの新規発行
			if (!PlayerPrefs.HasKey ("UserId")) {
				CreateUserID();
				Debug.Log ("ID作成");
			} else {
				GetUserId ();
				Debug.Log ("ID読み込み");
			}

			NCMB_.SaveAsync ();
			PlayerPrefs.Save ();

		}
		
		// Update is called once per frame
		void Update () {
			
		}

		//ID発行
		void CreateUserID(){

			//検索条件を0でない値にセット
			NCQ_server.WhereNotEqualTo ("LastUserId", 0);
			//検索し、リスト化して処理実行
			NCQ_server.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
				if (e != null) {
					//検索失敗時の処理
				} else {
					//検索成功時、見つかったObjを変数として保持
					foreach (NCMBObject obj in objList) {
						NCMB_server = obj;
						NCMB_server.ObjectId = obj.ObjectId;

						NCMB_server.FetchAsync ((NCMBException f) => {        
							if (f == null) {
								//成功時の処理
								//自身をユーザーとして登録
								num_id = System.Convert.ToInt32(NCMB_server["LastUserId"]);
								num_id += 1;

								NCMB_["UserId"] = num_id;
								NCMB_server["LastUserId"] = num_id;
								NCMB_["ClickCount"] = 0;

								NCMB_.SaveAsync();
								NCMB_server.SaveAsync();

								//ローカルにユーザーIdを保存
								PlayerPrefs.SetInt("UserId",num_id);
								PlayerPrefs.Save();

								flg_accept = true;
							}               
						});
					}
				}
			});
		}

		//IDを検索して取得
		void GetUserId(){
			num_id = PlayerPrefs.GetInt ("UserId");

			//検索条件をローカルで持っていたユーザーIDにセット
			NCQ_.WhereEqualTo ("UserId", num_id);
			//検索し、リスト化して処理実行
			NCQ_.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
				if (e != null) {
					//検索失敗時の処理
				} else {
					//検索成功時、見つかったObjを変数として保持
					foreach (NCMBObject obj in objList) {
						//オブジェクトの新規作成をなかったことにして、代わりにサーバーからデータを取得
						NCMB_.DeleteAsync((NCMBException g) =>{
							if(g == null){
								NCMB_ = obj;
								//取得したデータに置き換え
								NCMB_.ObjectId = obj.ObjectId;
								flg_accept = true;
							}
						});
					}
				}
			});
		}

		/// <summary>
		/// データをサーバーに送信する
		/// </summary>
		/// <param>string:キーの名前,float格納する値</param>
		public void SendData(string arg_key,float arg_num){
			NCMB_ [arg_key] = arg_num;
			NCMB_.SaveAsync ();
		}

	}
			
}
