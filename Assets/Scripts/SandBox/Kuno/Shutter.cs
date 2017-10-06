using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace ToyBox.Kuno{
	public class Shutter : MonoBehaviour {

		//このシャッターが開くのは初めてか？
		bool m_flg_isInitOpen = true;
		//このシャッターが閉じるのは初めてか？
		bool m_flg_isInitClose = true;
		//このボタンが押された時に再生するカメラムーブのID
		[SerializeField]
		int m_num_cameraMoveId;

		//このオブジェクトにカメラが注視している時間(遷移も含む)
		[SerializeField]
		float m_num_attentionTime;

		//このオブジェクトの初期相対Y座標
		float m_pos_initY;
		//このオブジェクトの終着Y座標
		float m_pos_endY;

		//このシャッターのステータス
		enum Status{
			open,
			close,
			up,
			down
		}
		Status m_enu_status;



		// Use this for initialization
		void Start () {
			m_pos_initY = transform.position.y;
			m_pos_endY = m_pos_initY + 2;

		}
		
		// Update is called once per frame
		void Update () {



			switch (m_enu_status) {
			case Status.down:
				if (transform.position.y > m_pos_initY) {
					transform.Translate (new Vector3 (0, -0.05f, 0));
				} else {
					m_enu_status = Status.close;
				}

				break;

				break;
			case Status.up:
				if (transform.position.y < m_pos_endY) {
					transform.Translate (new Vector3 (0, 0.05f, 0));
				} else {
					m_enu_status = Status.open;
				}

				break;
			}

		}



		public IEnumerator OpenMoveCamera(){
			

			if(m_flg_isInitOpen){
				CameraPosControll.Instance.SetTargetID (m_num_cameraMoveId);
				CameraPosControll.Instance.StartTargetMove ();
			}

			yield return new WaitForSeconds (m_num_attentionTime / 2);
			m_enu_status = Status.up;

			yield return new WaitForSeconds (m_num_attentionTime);

			if (m_flg_isInitOpen) {
				CameraPosControll.Instance.SetTargetID (0);
				CameraPosControll.Instance.StartTargetMove ();
				m_flg_isInitOpen = false;
			}


			yield break;
		}

		public IEnumerator CloseMoveCamera(){
			if (!isActiveAndEnabled)
				yield break;
			
			if (m_flg_isInitClose) {
				CameraPosControll.Instance.SetTargetID (m_num_cameraMoveId);
				CameraPosControll.Instance.StartTargetMove ();
			}

			yield return new WaitForSeconds (m_num_attentionTime / 2);
			m_enu_status = Status.down;

			yield return new WaitForSeconds (m_num_attentionTime);

			if(m_flg_isInitClose){
				CameraPosControll.Instance.SetTargetID (0);
				CameraPosControll.Instance.StartTargetMove ();
				m_flg_isInitClose = false;
			}

			yield break;
		}


	}
}
