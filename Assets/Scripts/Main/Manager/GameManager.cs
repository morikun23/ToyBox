using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class GameManager : MonoBehaviour {

		#region Singleton実装
		private static GameManager m_instance;

		public static GameManager Instance {
			get {
				if(m_instance == null) {
					m_instance = FindObjectOfType<GameManager>();
				}
				return m_instance;
			}
		}

		private GameManager() { }
		#endregion

		AudioNS.AudioManager m_audioManager;

		Logic.LogicManager m_logicManager;
		View.ViewManager m_viewManager;

		// Use this for initializationm
		void Start() {
			m_audioManager = AudioNS.AudioManager.Instance;
			
			m_logicManager = new Logic.LogicManager();
			m_viewManager = new View.ViewManager();

			m_audioManager.Initialize();

			m_logicManager.Initialize();
			m_viewManager.Initialize(m_logicManager);
		}

		// Update is called once per frame
		void Update() {
			m_audioManager.UpdateByFrame();
			
		}
	}
}