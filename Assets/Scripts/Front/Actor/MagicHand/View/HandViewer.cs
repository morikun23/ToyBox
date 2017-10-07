using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class HandViewer : AnimationObject {

		//横のスケールのバッファ
		private float xScaleBuf;

		public void Initialize(Hand arg_hand) {
			xScaleBuf = m_transform.localScale.x;
		}

		public void UpdateByFrame(Hand arg_hand) {
			
		}

		/// <summary>
		/// アームの向きに合わせて角度を調整する
		/// </summary>
		/// <param name="arg_angle">角度</param>
		public void SetRotation(float arg_angle) {
			m_transform.eulerAngles = Vector3.forward * arg_angle;
		}
	}
}