using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class UIManager : MonoBehaviour {

		public InputManager m_inputManager { get; private set; }

		public void Initialize(MainScene arg_mainScene) {
			m_inputManager = new InputManager();
			m_inputManager.Initialize(arg_mainScene);
		}

		public void UpdateByFrame(MainScene arg_mainScene) {
			m_inputManager.UpdateByFrame(arg_mainScene);
		}
	}
}