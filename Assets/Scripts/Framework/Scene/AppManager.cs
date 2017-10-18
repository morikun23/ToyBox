﻿//担当：森田　勝
//概要：アプリケーション内のパブリック機能および
//　　　パブリックなマネージャーを管理しているクラス
//　　　ユーティリティを使用するためにはこのクラスにアクセスする
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class AppManager : MonoBehaviour {

		#region Singleton実装
		private static AppManager m_instance;

		public static AppManager Instance {
			get {
				if (m_instance == null) {
					m_instance = new GameObject("AppManager").AddComponent<AppManager>();
					m_instance.Initialize();
				}
				return m_instance;
			}
		}

		private AppManager() { }
		#endregion

		//オーディオ環境
		public AudioManager m_audioManager { get; private set; }

		//フェード環境
		public Fade m_fade { get; private set; }

		//カメラ操作
		public CameraPosController m_camera { get; private set; }

		//時間環境
		public TimeManager m_timeManager { get; private set; }

		/// <summary>
		/// 初期化
		/// </summary>
		void Initialize() {
			m_audioManager = new GameObject("Audio").AddComponent<AudioManager>();
			m_audioManager.Initialize();
			m_fade = FindObjectOfType<Fade>();

			m_camera = CameraPosController.Instance;
			m_timeManager = new TimeManager();
			m_timeManager.Initialize();

			if (!m_fade) {
				m_fade = Instantiate(Resources.Load<GameObject>("Effect/FadeCanvas")).GetComponentInChildren<Fade>();
			}
			m_fade.Initialize();
			DontDestroyOnLoad(this.gameObject);
			DontDestroyOnLoad(m_audioManager.gameObject);
			DontDestroyOnLoad(m_fade.transform.parent.gameObject);
		}

	}
}