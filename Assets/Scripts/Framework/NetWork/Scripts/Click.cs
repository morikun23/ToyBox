using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using ToyBox;

namespace ToyBox.Kuno{
	public class Click : MonoBehaviour {

		[SerializeField]
		int cnt_click = 0;

		// Use this for initialization
		void Start () {
			StartCoroutine (GetNCMB());
		}

		//NCMBObjectから必要な値を取得
		void NCMBInit(){
			ToyBoxNCMB.NCMB_.FetchAsync ((NCMBException e) => {
				if(e == null){
					cnt_click = System.Convert.ToInt16(ToyBoxNCMB.NCMB_ ["ClickCount"]);
				}
			});
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetMouseButtonDown(0)){
				cnt_click++;
				ToyBoxNCMB.NCMB_ ["ClickCount"] = cnt_click;
				ToyBoxNCMB.NCMB_.SaveAsync ();
			}
		}

		//NCMBObjectの取得完了を待つ
		IEnumerator GetNCMB(){

			while (!ToyBoxNCMB.flg_accept) {
				yield return null;
			}

			Debug.Log ("ID取得完了、データ取得...");
			NCMBInit ();
			yield break;
		}
	}
}