using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_Swipe : MonoBehaviour {

	Vector3[] pos_input = new Vector3[10];
	Vector3[] pos_inputScreen = new Vector3[10];
	Vector3[] pos_inputBefore = new Vector3[10];
	Vector3[] pos_inputScreenBefore = new Vector3[10];
	Vector3[] pos_init = new Vector3[10];
	Vector3 num_size;

	public Transform pos_rotatePoint;

	bool[] flg_start = new bool[10];
	bool[] flg_move = new bool[10];
	int[] num_rotateDirection = new int[10];

	// Use this for initialization
	public void Start () {
		//サイズの取得
		if (GetComponent<BoxCollider2D> ()) {
			num_size = GetComponent<BoxCollider2D>().bounds.size;
		} else if(GetComponent<CircleCollider2D> ()){
			num_size = GetComponent<CircleCollider2D>().bounds.size;
		}else if(GetComponent<SpriteRenderer>()){
			num_size = GetComponent<SpriteRenderer> ().bounds.size;
		}

		//回転の中心を取得(Hieralkey上で何もなければ)
		if(pos_rotatePoint == null){
			pos_rotatePoint = transform;
		}
	}
	
	// Update is called once per frame
	public void Update () {

		int baf_i = 0;
		foreach (Touch t in Input.touches) {

			baf_i++;
			//タッチ位置を取得
			//１フレーム前のタッチ座標を格納
			pos_inputBefore[baf_i] = pos_input[baf_i];
			pos_inputScreenBefore [baf_i] = pos_inputScreen [baf_i];
			pos_inputScreen[baf_i] = t.position;
			pos_input[baf_i] = Camera.main.ScreenToWorldPoint (t.position);


			switch(t.phase){
			case TouchPhase.Began:
				if (!CheckHitPoint (pos_input[baf_i]))
					continue;
				Started ();
				pos_init[baf_i] = pos_input[baf_i];
				flg_start[baf_i] = true;
				//Debug.Log ("START");
				break;
			
			case TouchPhase.Moved:
				if (!flg_start [baf_i])
					continue;
				//Debug.Log ("FINGER MOVEING");
				Moving (t.deltaPosition);
				CheckRotate (t,baf_i);
				flg_move [baf_i] = true;
				break;
			
			case TouchPhase.Ended:
				if (!flg_start [baf_i])
					continue;

				if (!CheckMove (baf_i)) {
					SwipeE ();
					//Debug.Log ("SWIPE END");
				} else {
					TouchE ();
					//Debug.Log ("TOUCH END");
				}
				flg_start [baf_i] = false;
				flg_move [baf_i] = false;
				num_rotateDirection [baf_i] = 0;

				pos_input [baf_i] = Vector3.zero;
				pos_inputBefore [baf_i] = Vector3.zero;
				break;
			}

			if (flg_move[baf_i] && flg_start[baf_i]) {
				if(num_rotateDirection[baf_i] == 1){
					RightR (1);
				}else if(num_rotateDirection[baf_i] == -1){
					LeftR (-1);
				}
				break;
			}
		}
	}

	bool CheckHitPoint(Vector3 pos){
		//タッチ箇所がオブジェクトと重なっているかチェック
		if (pos.x <= pos_rotatePoint.position.x + num_size.x / 2 && pos.x >= pos_rotatePoint.position.x - num_size.x / 2 &&
			pos.y <= pos_rotatePoint.position.y + num_size.y / 2 && pos.y >= pos_rotatePoint.position.y - num_size.y / 2) {
			return true;
		}
		return false;
	}

	//タッチの許容範囲かどうかの計測
	bool CheckMove(int i){
		if(Mathf.Abs(pos_input[i].x - pos_init[i].x) <= 1 &&
			Mathf.Abs(pos_input[i].y - pos_init[i].y) <= 1){
			return true;
		}
		return false;
	}

	void CheckRotate(Touch t,int i){
		Vector2 baf_pos = pos_rotatePoint.position;
		float baf_rotBef = Vector2.Angle(pos_rotatePoint.position,pos_inputBefore[i] - pos_rotatePoint.position);
		float baf_rotNew = Vector2.Angle(pos_rotatePoint.position,pos_input[i] - pos_rotatePoint.position);

		Debug.DrawRay (pos_rotatePoint.position,pos_input[i] - pos_rotatePoint.position);
		Debug.Log (baf_rotNew - baf_rotBef);

		if(baf_rotNew - baf_rotBef > 5 && ((baf_rotBef > 0 && baf_rotNew > 0) || (baf_rotBef < 0 && baf_rotNew < 0))){
			num_rotateDirection[i] = 1;
		}
		else if(baf_rotNew - baf_rotBef < -5 && ((baf_rotBef > 0 && baf_rotNew > 0) || (baf_rotBef < 0 && baf_rotNew < 0))){
			num_rotateDirection[i] = -1;
		}
	}

	public virtual void Started(){}
	public virtual void Moving(Vector2 direc){}
	public virtual void TouchE(){}
	public virtual void SwipeE(){}
	public virtual void RightR(float dist){}
	public virtual void LeftR(float dist){}



}
