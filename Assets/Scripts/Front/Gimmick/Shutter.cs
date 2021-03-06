﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace ToyBox{
	public class Shutter : MonoBehaviour {

		//このシャッターが開くのは初めてか？
		//bool m_flg_isInitOpen = true;
		//このシャッターが閉じるのは初めてか？
		//bool m_flg_isInitClose = true;
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

        public int m_num_returnID;


        //InputManager m_InputManager;

        // Use this for initialization
        void Start () {
			m_pos_initY = transform.position.y;
			m_pos_endY = m_pos_initY + 2;

           // m_InputManager = FindObjectOfType<InputManager>();
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


//            if (m_flg_isInitOpen){
//				CameraPosController.Instance.SetTargetAndStart(m_num_cameraMoveId);
//
//				m_InputManager.InputEnabled(false);
//                yield return new WaitForSeconds (m_num_attentionTime / 2);
//			}


			m_enu_status = Status.up;

            //新しい音でございます
            AudioManager.Instance.QuickPlaySE("SE_Shutter_open");

			yield return new WaitForSeconds (m_num_attentionTime);

//			if (m_flg_isInitOpen) {
//				CameraPosController.Instance.SetTargetAndStart (m_num_returnID);
//
//				m_InputManager.InputEnabled(true);
//                m_flg_isInitOpen = false;
//			}

            yield break;
		}

		public IEnumerator CloseMoveCamera(){
            

            if (!isActiveAndEnabled)
				yield break;
			
//			if (m_flg_isInitClose) {
//				CameraPosController.Instance.SetTargetAndStart(m_num_cameraMoveId);
//				m_InputManager.InputEnabled (false);
//                yield return new WaitForSeconds (m_num_attentionTime / 2);
//			}
	
			m_enu_status = Status.down;

            AudioManager.Instance.QuickPlaySE("SE_Shutter_close");

			yield return new WaitForSeconds (m_num_attentionTime);

//			if(m_flg_isInitClose){
//				CameraPosController.Instance.SetTargetAndStart (m_num_returnID);
//				m_InputManager.InputEnabled (true);
//                m_flg_isInitClose = false;
//			}

            
            yield break;
		}


	}
}
