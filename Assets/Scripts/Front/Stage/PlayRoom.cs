using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class PlayRoom : MonoBehaviour {

		//こいつの部屋番号 インスペクターで設定
		[SerializeField]
		private int m_id;	

		public int Id {
			get {
				return m_id;
			}
		}

		//子オブジェクトのギミック達　初期化時に入れ子？
		private readonly List<Transform> m_gimmicks = new List<Transform>();
		
		// Use this for initialization
		void Start() {
			
			//TODO:Gimmickをタグもしくは基底クラスで区別させる
			foreach (Transform gimmick in this.transform) {
				gimmick.gameObject.SetActive(true);
				m_gimmicks.Add(gimmick);
			}
		}

		
		void OnEnable() {
			
		}
	}

}