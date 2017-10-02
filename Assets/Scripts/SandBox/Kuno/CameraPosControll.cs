//担当：久野　直之
//概要：カメラのスムーズな移動を実現する為のクラスです。
//		ManagerとしてHierarchy上に配置してください。
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace ToyBox.Kuno{
	public class CameraPosControll : MonoBehaviour {

		#region Singleton実装
		private static CameraPosControll m_instance;

		public static CameraPosControll Instance {
			get {
				if (m_instance == null) {
					m_instance = FindObjectOfType<CameraPosControll> ();
				}
				return m_instance;
			}
		}

		private CameraPosControll() { }
		#endregion

		//ターゲットになるカメラ
		[SerializeField]
		Camera m_obj_camera;
		//カメラが追従するターゲット
		[SerializeField]
		GameObject m_obj_cameraTarget;


		//カメラの各座標への移動に使用するステータ群。
		//配列No.をIDとし、各IDを指定して再生...といった使用をする。
		//なお、ID0番のみ追従モードなる物で使用するので必須だが、
		//pos_targetの値を設定する必要はない。
		[System.Serializable]
		struct CameraStatus{
			//カメラ移動に使用するSinカーブ(出発点から到着点までの移動力)
			public AnimationCurve cur_move;
			//何フレーム使用して移動するか
			public int num_complateFrame;
			//実際に向かう座標(GameObjectで指定する事を想定しているので、Transformで指定している)
			public Transform pos_target;
			//カメラの指定スケール(デフォルトで5)
			public float num_scale;
		}
		[SerializeField]
		CameraStatus[] m_str_cameraStatus = new CameraStatus[1];

		//現在再生するID
		public int num_id = 0;
		//モード。true=追従、false=座標固定
		[SerializeField]
		bool m_flg_isHomingMode = true;

		//Homing専用。移動が完了したらtrueになる
		bool m_flg_isComplate = false;

		public bool flg_hoge;


		// Use this for initialization
		void Start () {
			m_str_cameraStatus [0].pos_target = m_obj_cameraTarget.transform;
			SetTargetID (0);
			StartTargetMove ();
		}
		
		// Update is called once per frame
		void Update () {
			//isHomingModeとisComplateがtrueなら、カメラをターゲットの位置に固定する
			if(m_flg_isHomingMode && m_flg_isComplate){
				m_obj_camera.transform.position = new Vector3 (
					m_obj_cameraTarget.transform.position.x,
					m_obj_cameraTarget.transform.position.y,
					-10);
			}

			if(flg_hoge){
				flg_hoge = false;
				StartTargetMove ();
			}
		}


		IEnumerator MoveToTarget(){
			if (!isActiveAndEnabled)
				yield break;

			CameraStatus baf_status;
			float baf_size = m_obj_camera.orthographicSize;

			//0番は追従専用のモードなので、指定されたらHomingModeをOnにする
			if (num_id == 0) {
				m_flg_isHomingMode = true;
			} else {
				//その他はfalseにする
				m_flg_isHomingMode = false;
			}

			//HomingModeによって対象を分岐する
			if (!m_flg_isHomingMode) {
				baf_status = m_str_cameraStatus [num_id];
			} else {
				baf_status = m_str_cameraStatus [0];
			}

			Vector3 baf_pos = m_obj_camera.transform.position;

			//対象の座標に向かってAnimationCurveを使用して遷移する
			for(int i = 1;i < baf_status.num_complateFrame;i++){
				m_obj_camera.transform.position = new Vector3 (
					baf_pos.x + (baf_status.pos_target.position.x - baf_pos.x) * baf_status.cur_move.Evaluate((float)i/(float)baf_status.num_complateFrame),
					baf_pos.y + (baf_status.pos_target.position.y - baf_pos.y) * baf_status.cur_move.Evaluate((float)i/(float)baf_status.num_complateFrame),
					-10
				);
				m_obj_camera.orthographicSize = baf_size + (baf_status.num_scale - baf_size) * baf_status.cur_move.Evaluate ((float)i/(float)baf_status.num_complateFrame);

				yield return null;
			}



			if(m_flg_isHomingMode)
				m_flg_isComplate = true;

			yield break;
		}


		/// <summary>
		/// 再生したいカメラムーブのIDを設定します。
		/// IDは配列のNo.と同期しています。
		/// 実際に再生する場合は、StartTargetMove関数を使用してください。
		/// </summary>
		/// <param name="id">再生するカメラムーブのID</param>
		public void SetTargetID(int id){
			num_id = id;
			m_flg_isComplate = false;
		}

		/// <summary>
		/// 追従モードの際、カメラが追従するオブジェクトを設定します。
		/// この値はInspector内で初期設定を行う事もできます。
		/// </summary>
		/// <param name="obj">設定するGameObject</param>
		public void SetTargetObject(GameObject obj){
			m_obj_cameraTarget = obj;
			m_str_cameraStatus [0].pos_target = obj.transform;
			m_flg_isComplate = false;
		}

		/// <summary>
		/// SetTargetID関数で設定したIDのカメラムーブを再生します。
		/// IsHomingModeの値がtrueなら、強制的にID0が再生されます。
		/// </summary>
		public void StartTargetMove(){
			StartCoroutine (MoveToTarget());
		}
			
		/// <summary>
		/// モードを追従モードか座標指定モードにするかを設定します。
		/// true:追従モード。SetTargetObject及びInspector内で指定したObjを追従する。
		/// false:座標指定モード。SetTargetIDで指定された座標にカメラを固定する。
		/// </summary>
		public void IsHomingMode(bool IsHoming){
			m_flg_isHomingMode = IsHoming;
		}
	}
}