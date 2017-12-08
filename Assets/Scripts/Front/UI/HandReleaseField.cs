using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace ToyBox{
	public class HandReleaseField : TouchActor {

		Playable m_scr_playable;
		BoxCollider2D col_;

		public void Start(){
			base.Start ();
			col_ = GetComponent<BoxCollider2D> ();

		}

		public void Update(){
			if (isAbleHandShot()) {
				col_.enabled = false;
			} else {
				col_.enabled = true;
			}


		}

		public override void TouchStart (Vector2 pos){
			base.TouchStart (pos);
			if (!isAbleHandShot()) {
				InputManager.Instance.GetPlayableCharactor ().Release();
			}
		}

		bool isAbleHandShot(){
			return InputManager.Instance.GetPlayableCharactor ().IsAbleReach () && !InputManager.Instance.GetPlayableCharactor ().m_playableHand.IsGrasping ();
		}
	}
}
