//担当：森田　勝
//概要：シーンを管理するクラス
//　　　１シーン管理でのシーンの処理のトリガーとなります。
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class SceneManager : MonoBehaviour {
		
		//現在のシーン
		public Scene m_currentScene { get; private set; }

		//１シーンを進行するための現在のフェイズ
		private enum Phase {
			ENTER,
			UPDATE,
			EXIT
		}

		//現在のフェイズ
		private Phase m_currentPhase;

		//次に予約されているシーン
		private Scene m_nextScene;

		//シーン繊維をするためのフェードオブジェクト
		private Fade m_fade { get; set; }

		/// <summary>
		/// 生成時にデフォルトのシーンを設定させる
		/// </summary>
		/// <param name="arg_defaultScene"></param>
		public SceneManager(Scene arg_defaultScene) {
			m_currentScene = arg_defaultScene;
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			m_fade = Instantiate(Resources.Load<GameObject>("Effect/FadeCanvas")).GetComponentInChildren<Fade>();
			m_fade.Initialize();
			m_fade.Fill(Color.black);
			m_nextScene = null;
			m_currentPhase = Phase.ENTER;
		}

		/// <summary>
		/// 更新
		/// 処理内容はフェイズ別で分かれる
		/// ENTER：フェードイン、シーン開始時の処理
		/// UPDATE：更新
		/// EXIT：フェードアウト、シーン終了時の処理
		/// </summary>
		public void UpdateByFrame() {
			if (m_fade.isActive) {	m_fade.UpdateByFrame(); return; }

			switch (m_currentPhase) {
				case Phase.ENTER:
				m_currentScene.OnEnter();
				m_fade.StartFade(new FadeIn() , Color.black , 1.5f);
				m_currentPhase = Phase.UPDATE;
				break;
				case Phase.UPDATE:
				
				m_currentScene.OnUpdate();
				break;
				case Phase.EXIT:
				m_currentScene.OnExit();
				m_currentScene = m_nextScene;
				m_nextScene = null;
				m_currentPhase = Phase.ENTER;
				break;
			}
		}

		/// <summary>
		/// シーン遷移をおこなう
		/// </summary>
		/// <param name="arg_nextScene">次に再生するシーン</param>
		public void SceneTransition(Scene arg_nextScene) {
			m_nextScene = arg_nextScene;
			m_currentPhase = Phase.EXIT;
			m_fade.StartFade(new FadeOut() , Color.black , 3.0f);
		}

		/// <summary>
		/// シーン遷移をおこなう
		/// フェードの色を決められる
		/// </summary>
		/// <param name="arg_nextScene"></param>
		/// <param name="arg_fadeColor"></param>
		public void SceneTransition(Scene arg_nextScene,Color arg_fadeColor) {
			m_nextScene = arg_nextScene;
			m_currentPhase = Phase.EXIT;
			m_fade.StartFade(new FadeOut() , arg_fadeColor , 3.0f);
		}
	}
}