//担当：森田　勝
//概要：ゲーム内のクラスを管理します。
//　　　このクラス自身がUnity側からのトリガーとなります。
//　　　以下、このクラスが他のクラスのトリガーとして使用してください。
//参考：特になし

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

		//オーディオ環境
		public AudioManager m_audioManager { get; private set; }

		//シーン環境
		public SceneManager m_sceneManager { get; private set; }
		
		//TODO:リソース環境の追加
		//TODO:クラウド環境の追加
	
		// Use this for initializationm
		void Start() {
			m_audioManager = AudioManager.Instance;
			m_sceneManager = new SceneManager(new Main.MainScene());
			
			m_audioManager.Initialize();
			m_sceneManager.Initialize();

		}

		// Update is called once per frame
		void FixedUpdate() {

			m_sceneManager.UpdateByFrame();
			m_audioManager.UpdateByFrame();
		}
	}
}