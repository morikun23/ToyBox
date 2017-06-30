using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
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

		public AudioManager m_audioManager { get; private set; }

		public SceneManager m_sceneManager { get; private set; }
		
		// Use this for initializationm
		void Start() {
			m_audioManager = AudioManager.Instance;
			m_sceneManager = new SceneManager();

			m_audioManager.Initialize();
			m_sceneManager.Initialize();

		}

		// Update is called once per frame
		void Update() {
			m_audioManager.UpdateByFrame();
			m_sceneManager.UpdateByFrame();
		}
	}
}