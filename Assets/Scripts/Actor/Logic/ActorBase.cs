using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public abstract class ActorBase {

		//座標
		public Vector2 m_position;

		//回転
		public float m_rotation;

		//幅
		public float m_width;

		//高さ
		public float m_height;

		//アクティブ状態（動作しているか）
		public bool m_isActive;

		protected ActorBase(Vector2 arg_position,float arg_rotation,float arg_width,float arg_height,bool arg_isActive = true){
			m_position = arg_position;
			m_rotation = arg_rotation;
			m_width = arg_width;
			m_height = arg_height;
			m_isActive = arg_isActive;
		}

		public void SetActive(bool arg_value) {
			m_isActive = arg_value;
		}

	}
}