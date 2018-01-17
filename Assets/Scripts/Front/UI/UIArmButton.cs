using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ToyBox {
	public class UIArmButton : UIDraggableButton {

		//Imageコンポーネント
		public Image m_image;

		//掴んでいるときのSprite
		public Sprite m_armGraspSprite;

		//放しているときのSprite
		public Sprite m_armReleaseSprite;

		protected override void OnPressed() {
			m_image.sprite = m_armGraspSprite;
		}

		protected override void OnReleased() {
			m_image.sprite = m_armReleaseSprite;
			transform.position = DefaultPosition;
		}
	}
}