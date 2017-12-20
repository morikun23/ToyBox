//概要：InputActorにタッチ情報を渡すクラス
//		Sceneに１つ配置し、Inspector上でm_num_maxHierarchyの値を編集する必要があります。
//		複数のTouchActorが重なっていた場合、深度(デフォルトでHierarchy上で下にあるオブジェクト程大きい)を確認し、
//		大きなオブジェクトを優先してタッチします。
//担当：久野

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox{
	public class InputManager : MonoBehaviour {

		#region Singleton実装
		private static InputManager m_instance;

		public static InputManager Instance{
			get{
				if (m_instance == null){
					m_instance = FindObjectOfType<InputManager>();
				}
				return m_instance;
			}
		}

		private InputManager() { }
		#endregion

		private TouchActor[] m_scr_touchActor = new TouchActor[10];
		private bool[] m_flg_hit = new bool[10];
		private bool[] m_flg_swipe = new bool[10];

		private bool m_flg_active = true;

		[SerializeField,Tooltip("このコントローラーが入力を受け取るカメラ配列")]
		private Camera[] m_cam_judge;

		//Hierarchy上のツリー内で、InputActorが存在する最大深度
		//root/hoge/fuga/TouchActorとツリーが出来ているなら、値は３と入力します
		[SerializeField,Range(0,10),Tooltip("Hierarchy上で root/hoge/fuga/TouchActorとツリーが出来ているなら、値は３と入力します")]
		private int m_num_maxHierarchy;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update() {

			if (!m_flg_active)
				return;

			foreach(Camera cam in m_cam_judge){
				//指の数だけ判定を取る
				foreach(Touch t in Input.touches){
					//この指が何にもヒットしていないなら、LineTraceを実行
					if(!m_flg_hit[t.fingerId])
						m_scr_touchActor[t.fingerId] = LineTrace (cam.ScreenToWorldPoint (t.position),t.fingerId);

					//指の状態によって条件分岐
					switch(t.phase){
					//タッチ開始
					case TouchPhase.Began:
						if (m_scr_touchActor[t.fingerId])
							m_scr_touchActor[t.fingerId].TouchStart(t.position);
						break;
					//タッチして静止
					case TouchPhase.Stationary:
						if (m_scr_touchActor[t.fingerId])
							m_scr_touchActor[t.fingerId].TouchStay (t.position);
						break;
					//スワイプ
					case TouchPhase.Moved:
						if (m_scr_touchActor[t.fingerId])
							m_scr_touchActor[t.fingerId].Swipe(t.position,t.deltaPosition);
						m_flg_swipe[t.fingerId] = true;
						break;
					//タッチorスワイプ終了
					case TouchPhase.Ended:
						//スワイプしたかどうかで起動する関数を変更
						if (m_scr_touchActor [t.fingerId]) {
							if (m_flg_swipe [t.fingerId]) {
								m_scr_touchActor [t.fingerId].SwipeEnd (t.position);
							} else {
								m_scr_touchActor [t.fingerId].TouchEnd (t.position);
							}
						}
						//登録した指をリセット
						m_flg_swipe [t.fingerId] = false;
						m_flg_hit [t.fingerId] = false;
						m_scr_touchActor [t.fingerId] = null;
						break;
					}
				}
			}

		}

		//LineTraceを行い、複数のオブジェクトがヒットした場合は深度判定を行う
		TouchActor LineTrace(Vector2 pos,int id){
			RaycastHit2D[] rays = Physics2D.RaycastAll (pos,Vector3.forward);

			//そもそもオブジェクトがかかってないならnull
			if (rays == null)
				return null;

			//判定用変数の初期化
			RaycastHit2D ray_top = new RaycastHit2D();
			TouchActor baf_compActor = null;
			List<int> baf_depth = new List<int>(0);
			for(int i = 0;i < m_num_maxHierarchy;i ++){
				baf_depth.Add (0);
			}

			//rayに当たったオブジェクトの数だけ判定
			foreach(RaycastHit2D ray in rays){
				TouchActor baf_touchActor;

				//当たったオブジェクトがTouchActorなら処理
				if (ray.transform.GetComponent<TouchActor> ()) {
					baf_touchActor = ray.transform.GetComponent<TouchActor> ();

					int baf_cnt = 0;
					//深度判定
					foreach(int val in baf_touchActor.GetDepth()){

						if (baf_cnt >= baf_depth.Count) break;

						if (baf_depth [baf_cnt] <= val) {
							baf_compActor = baf_touchActor;
							baf_depth = baf_touchActor.GetDepth ();
							baf_cnt++;
							continue;
						}
						baf_cnt++;
					}
				}
			}

			//深度判定によって得られた１つのTouchActorを操作オブジェクトとして登録
			if(baf_compActor){
				m_flg_hit [id] = true;
			}

			return baf_compActor;
		}

		/// <summary>
		/// Hierarchy上で、InputActorが存在する最大深度を返します。
		/// </summary>
		public int GetMaxHeirarchy(){
			return m_num_maxHierarchy;
		}

		/// <summary>
		/// InputManagerからの指示の有効⇔無効を設定します。
		/// </summary>
		public void InputEnabled(bool enabled){
			m_flg_active = enabled;
		}

		/// <summary>
		/// <para>このコントローラーが動かしているプレーヤーを返します。</para>
		/// </summary>
		/// <returns>The playable charactor.</returns>
		//　旧InputManagerの機能の為、このスクリプト内では未使用
		public Playable GetPlayableCharactor(){
			return null;
		}
	}
}
