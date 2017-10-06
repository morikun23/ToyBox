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
			UpdateRotation(arg_hand);
			
			m_spriteRenderer.enabled = arg_hand.m_owner.Arm.m_isActive;
			
		}

		private void UpdateRotation(Hand arg_hand) {
			ArmComponent arm = arg_hand.m_owner.Arm;

			float x0 = arm.GetBottomPosition().x;
			float y0 = arm.GetBottomPosition().y;
			float x1 = arg_hand.m_transform.position.x;
			float y1 = arg_hand.m_transform.position.y;

			//Atan2でラジアン値を取得
			float theta = Mathf.Atan2(y1 - y0 , x1 - x0);

			//度数値に修正
			float angle = theta * Mathf.Rad2Deg;

			//角度を反映
			SetRotation(angle);
		}

		/// <summary>
		/// 回転角度を設定する
		/// </summary>
		/// <param name="arg_angle">角度</param>
		public void SetRotation(float arg_angle) {
			m_transform.eulerAngles = Vector3.forward * arg_angle;
		}
	}
}