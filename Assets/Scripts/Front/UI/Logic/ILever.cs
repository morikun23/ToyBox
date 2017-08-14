using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public interface ILever {

		void Initialize();
		void UpdateByFrame();

		void OnPointerEnter();
		void OnPointerStay();
		void OnPOinterExit();
		void OnPointerUp();
		void OnPointerDown();
	}
}