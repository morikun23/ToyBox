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

		AudioNS.AudioManager m_audioManager;

		// Use this for initialization
		void Start() {
			m_audioManager = AudioNS.AudioManager.Instance;
			m_audioManager.Initialize();
		}

		// Update is called once per frame
		void Update() {
			m_audioManager.UpdateByFrame();
		}
	}
}