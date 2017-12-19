using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace ToyBox{
	public class Switch : MonoBehaviour {

		//このボタンで開くシャッター
		[SerializeField]
		Shutter m_scr_shutter;

		//通常時と押されている時、それぞれのスプライト
		[SerializeField]
		Sprite m_spr_newtoral;
		[SerializeField]
		Sprite m_spr_pushed;

		//自身の必要なコンポーネント
		SpriteRenderer m_sRend_;
		BoxCollider2D col_;

		void Start(){
			m_sRend_ = GetComponent<SpriteRenderer> ();
			col_ = GetComponent<BoxCollider2D> ();
		}

		// Update is called once per frame
		void Update () {
		}

		void OnTriggerEnter2D(Collider2D arg_col){
			m_sRend_.sprite = m_spr_pushed;
			StartCoroutine (m_scr_shutter.OpenMoveCamera());

            //新しい音でございます
            AudioManager.Instance.QuickPlaySE("SE_Switch_push");
		}
		void OnTriggerExit2D(Collider2D arg_col){

			if(col_.IsTouchingLayers())
				return;

			m_sRend_.sprite = m_spr_newtoral;
			StartCoroutine (m_scr_shutter.CloseMoveCamera());

            AudioManager.Instance.QuickPlaySE("SE_Switch_cancel");
		}



	}

}