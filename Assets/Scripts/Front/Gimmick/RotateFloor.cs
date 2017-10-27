//作成者：久野　直之
//概要：回転床についているグリップの情報を読み取り、回転を制御するクラスです。
//配列に格納された全てのグリップからプレーヤーの判定を取り、このクラスが入っているHierarchy上の
//オブジェクトを中心に回転します。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace ToyBox{
	public class RotateFloor : MonoBehaviour {

		//この回転床が参照するグリップ
		public List<MovableGlip> m_scr_MovableGrip;

		//回転に使用するAnimationCurve
		[SerializeField]
		AnimationCurve m_cur_rotateCurve;

		//回転が完了するまでのフレーム数
		[SerializeField]
		int m_num_rotateComp; 


		//この回転床のステータス
		public enum Status{
			NEUTRAL,
			GRABBED
		}
		public Status m_enu_status;
		
		// Update is called once per frame
		void Update () {
			//掴まれてない時だけプレーヤーからのアクションを待つ
			if(m_enu_status == Status.NEUTRAL){
				//登録されたグリップの数だけ処理
				for(int i = 0;i < m_scr_MovableGrip.Count;i ++){
					//グリップにプレーヤーが捕まっていれば処理
					if(m_scr_MovableGrip[i].m_enu_state == MovableGlip.GripState.STAY){
						//回転を始める
						StartCoroutine (Rotate(m_scr_MovableGrip[i].transform.eulerAngles.z,i));
						m_enu_status = Status.GRABBED;
					}
				}
			}
		}

		IEnumerator Rotate(float arg_rotation,int arg_gripId){
			//掴んだグリップの角度を-179～180で取得
			int baf_rotationGrip = (int)Mathf.Round(arg_rotation);
			if(baf_rotationGrip > 180){
				baf_rotationGrip = baf_rotationGrip - 360;
			}
			//自身の角度を-179～180で取得
			int baf_rotationObj = (int)Mathf.Round(transform.eulerAngles.z);
			if(baf_rotationObj > 180){
				baf_rotationObj = baf_rotationObj - 360;
			}

			//既にこのコルーチンが走っている、または回転させる必要がなければ何もしないで終わる
			if (!isActiveAndEnabled || baf_rotationGrip == 0 || baf_rotationGrip == 180) {
				//プレーヤーが手を離すのを待つ
				while(m_scr_MovableGrip[arg_gripId].m_enu_state == MovableGlip.GripState.STAY){
					yield return null;
				}
				//再びアクション待ちに戻す
				m_enu_status = Status.NEUTRAL;
				yield break;
			}
				
			AudioSource source = AppManager.Instance.m_audioManager.CreateSe ("SE_RotateFloor_rotate");
			source.Play ();
		
			//グリップの角度が０になるまで(num_rotateCompの値だけ)処理
			for(int i = 1;i <= m_num_rotateComp;i ++){
				//AnimationCurveを利用して加速度的に回転
				if (baf_rotationGrip > 0) {
					transform.eulerAngles = new Vector3 (0, 0,baf_rotationGrip * m_cur_rotateCurve.Evaluate((float)i/m_num_rotateComp) - (baf_rotationGrip - baf_rotationObj));
				} else if (baf_rotationGrip <= 0) {
					transform.eulerAngles = new Vector3 (0, 0,baf_rotationGrip * m_cur_rotateCurve.Evaluate((float)i/m_num_rotateComp) - (baf_rotationGrip - baf_rotationObj));
				}

				//回転中はグリップから手を離させない
				m_scr_MovableGrip [arg_gripId].SetAbleRelease (false);
				yield return null;
			}

			//回転が終わったら離してよいことにする
			m_scr_MovableGrip[arg_gripId].SetAbleRelease(true);

			source = AppManager.Instance.m_audioManager.CreateSe ("SE_RotateFloor_rotateComp");
			source.Play ();

			//プレーヤーが手を離すのを待つ
			while(m_scr_MovableGrip[arg_gripId].m_enu_state == MovableGlip.GripState.STAY){
				yield return null;
			}
				
			//再びアクション待ちに戻す
			m_enu_status = Status.NEUTRAL;
			yield break;
		}
	}
}