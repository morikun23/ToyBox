using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class HandViewer : AnimationObject {

		//横のスケールのバッファ
		private float xScaleBuf;

		public void Initialize() {
			xScaleBuf = m_transform.localScale.x;
		}

		public void UpdateByFrame() {
			
		}
	}
}