﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;
using UnityEngine.EventSystems;

namespace ToyBox{
	public class HandReleaseField : TouchActor {

		Playable m_scr_playable;
		BoxCollider2D col_;

		private Playable player {
			get {
				if(m_scr_playable == null) {
					m_scr_playable = FindObjectOfType<Playable>();
				}
				return m_scr_playable;
			}
		}

		public new void Start(){
			base.Start ();
			col_ = GetComponent<BoxCollider2D> ();
		}

		public void Update(){

            transform.position = Camera.main.transform.position;

			if (player == null) return;

			if (isAbleHandShot()) {
				col_.enabled = false;
			} else {
				col_.enabled = true;
			}


		}

		protected override void TouchStart (PointerEventData data){
			base.TouchStart (data);

			if (player == null) return;
			if (!isAbleHandShot()) {
				//player.Release();
			}
		}

		bool isAbleHandShot(){
			//return player.IsAbleReach () && !player.m_playableHand.IsGrasping ();
			return false;
		}
	}
}
