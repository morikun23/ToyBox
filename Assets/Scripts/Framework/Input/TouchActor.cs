//概要：InputManagerからタッチを受け取るクラス
//		Hierarchy上で一番下にあるものから優先してタッチ判定が取られます
//		基本的にこのクラスを継承して運用してください
//担当：久野

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ToyBox{
	public class TouchActor : MonoBehaviour {

		//[SerializeField]
		private List<int> m_num_depth = new List<int>();

		// Use this for initialization
		public void Start () {
			//depthが初期値のままならアップデートする
			if(m_num_depth.Count == 0){
				UpdateDepth ();
			}
		}
		
		// Update is called once per frame
		void Update () {
		}

		/// <summary>
		/// <para>このオブジェクトがタッチされた時に１度だけ呼ばれる関数です。</para>
		/// <para>pos:タッチ時の指の座標が格納されます</para>
		/// </summary>
		public virtual void TouchStart(Vector2 pos){
			#region デバッグ用
			//Debug.Log (gameObject.name + ":Touch!!");

	//		if (GetComponent<Image> ()) {
	//			GetComponent<Image> ().color = Color.black;
	//		} else if(GetComponent<SpriteRenderer>()){
	//			GetComponent<SpriteRenderer> ().color = Color.black;
	//		}
			#endregion
		}

		/// <summary>
		/// <para>このオブジェクト上で指が静止している時に継続して呼ばれる関数です。</para>
		/// <para>pos:タッチ時の指の座標が格納されます</para>
		/// </summary>
		public virtual void TouchStay(Vector2 pos){
			#region デバッグ用
			//Debug.Log (gameObject.name + ":TouchStay!!");
	//		transform.eulerAngles += new Vector3 (0,0,1f);
			#endregion
		}

		/// <summary>
		/// <para>スワイプ中に継続して呼ばれる関数です。</para>
		/// <para>pos:タッチ時の指の座標が格納されます</para>
		/// <para>>deltaPos:１フレーム前からの指の移動量が格納されます</para>
		/// </summary>
		public virtual void Swipe(Vector2 pos,Vector2 deltaPos){
			#region デバッグ用
			//Debug.Log (gameObject.name + ":Swipe!!");
	//		transform.localScale += new Vector3 (deltaPos.x / 1000,deltaPos.y / 1000,1);
			#endregion
		}
			
		/// <summary>
		/// <para>１度も指を動かさずに指を離した際に１度だけ呼ばれる関数です。</para>
		/// <para>pos:タッチ時の指の座標が格納されます</para>
		/// </summary>
		public virtual void TouchEnd(Vector2 pos){
			#region デバッグ用
			//Debug.Log (gameObject.name + ":TouhEnd!!");
	//		if (GetComponent<Image> ()) {
	//			GetComponent<Image> ().color = Color.white;
	//		} else if(GetComponent<SpriteRenderer>()){
	//			GetComponent<SpriteRenderer> ().color = Color.white;
	//		}
	//		transform.eulerAngles = Vector3.zero;
	//		transform.localScale = new Vector3 (1,1,1);
			#endregion
		}

		/// <summary>
		/// <para>スワイプ後に指を離した際に１度だけ呼ばれる関数です。</para>
		/// <para>pos:タッチ時の指の座標が格納されます</para>
		/// </summary>
		public virtual void SwipeEnd(Vector2 pos){
			#region デバッグ用
			//Debug.Log (gameObject.name + ":SwipeEnd!!");
	//		if (GetComponent<Image> ()) {
	//			GetComponent<Image> ().color = Color.white;
	//		} else if(GetComponent<SpriteRenderer>()){
	//			GetComponent<SpriteRenderer> ().color = Color.white;
	//		}
	//		transform.eulerAngles = Vector3.zero;
	//		transform.localScale = new Vector3 (1,1,1);
			#endregion
		}

		/// <summary>
		/// <para>このオブジェクトの深度を手動で設定します。</para>
		/// <para>上位の配列からそれぞれ値は確認され、大きい数値の深度を持つオブジェクト程タッチの優先度も高くなります。</para> 
		/// <para>depth:設定するdepth</para>
		/// </summary>
		public void SetDepth(List<int> depth){
			m_num_depth = depth;
		}

		/// <summary>
		/// <para>このオブジェクトの深度を自動で更新します。</para>
		public void UpdateDepth(){
			Transform baf_root = transform.root;
			Transform baf_parent = transform;

			while(baf_root != baf_parent){
				m_num_depth.Insert (0,baf_parent.GetSiblingIndex());
				baf_parent = baf_parent.parent;
			}
			m_num_depth.Insert (0,baf_parent.GetSiblingIndex());

			for(int i = m_num_depth.Count;i <= InputManager.Instance.GetMaxHeirarchy();i ++){
				m_num_depth.Add (0);
			}
		}

		/// <summary>
		///	このオブジェクトの深度を取得します。
		/// </summary>
		/// <returns>深度</returns>
		public List<int> GetDepth(){
			return m_num_depth;
		}
	}
}
