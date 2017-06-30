using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class InputManager : MonoBehaviour{
		


		private static InputManager m_instance;

		public static InputManager Instance {
			get {
				if(m_instance == null) {
					m_instance = new GameObject("InputManager").AddComponent<InputManager>();
					Debug.Log("InputManager : Created");
				}
				return m_instance;
			}
		}


		public void Initialize() {
			
		}

		public void UpdateByFrame() {
			
			foreach(Touch touchInfo in Input.touches) {
				
			}
		}
	}
}