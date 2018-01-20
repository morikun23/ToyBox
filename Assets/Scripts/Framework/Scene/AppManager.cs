using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public sealed class AppManager : MonoBehaviour {

		#region Singleton
		private static AppManager m_instance;

		public static AppManager Instance {
			get {
				if (m_instance == null) {
					m_instance = FindObjectOfType<AppManager>();
				}
				return m_instance;
			}
		}

		private AppManager() { }
		#endregion

		//オーディオ環境
		[SerializeField]
		private AudioManager audioManager;
		public AudioManager m_audioManager { get { return audioManager; } }

		//フェード環境
		[SerializeField]
		private Fade fade;
		public Fade m_fade { get { return fade; } }

		//時間環境
		[SerializeField]
		private TimeManager timeManager;
		public TimeManager m_timeManager { get { return timeManager; } }

		//ユーザー情報
		private UserData m_user;
		public UserData user { get { return m_user; } }
		[SerializeField]
		private ToyBoxNCMB m_NCMB;
		public ToyBoxNCMB NCMB {get { return m_NCMB;}}

		/// <summary>
		/// 初期起動
		/// </summary>
		private void Awake() {
			if (Instance != this) {
				Destroy(this.gameObject);
				return;
			}

			#region NULLチェック
			if (m_audioManager == null) {
				Debug.LogError("[ToyBox]<color=red>AudioManager</color>が設定されていません");
			}

			if (m_fade == null) {
				Debug.LogError("[ToyBox]<color=red>Fade</color>が設定されていません");
			}

			if (m_timeManager == null) {
				Debug.LogError("[ToyBox]<color=red>TimeManager</color>が設定されていません");
			}
			#endregion

			DontDestroyOnLoad(this.gameObject);

			m_user = new UserData();
			m_user.Load();

			m_timeManager.Initialize();

			m_fade.Initialize();

			
		}
	}
}