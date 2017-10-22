//担当：森田　勝
//概要：メインシーン全体を管理するクラス
//　　　シーン内の処理はこのクラスをトリガーとしてください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox {
	public class MainScene : ToyBox.Scene {

		private Stage m_stage;

		private Player m_player;

		private InputManager m_inputManager;

		public override IEnumerator OnEnter() {
			//ステージの生成（どのステージなのかを区別させたい）
			m_stage = FindObjectOfType<Stage>();

			//プレイヤーの生成
			m_player = FindObjectOfType<Player>();
			m_stage.SetPlayer(m_player);

			//入力環境を初期化
			m_inputManager = GetComponent<InputManager>();
			m_inputManager.Initialize();

			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			
		}

		public override IEnumerator OnUpdate() {
			while (true) {
				if(m_player.GetCurrentState() != typeof(PlayerDeadState)){
					m_inputManager.UpdateByFrame();
				}
				if (m_stage.DoesPlayerReachGoal()) {
					break;
				}
				yield return null;
			}
		}

		public override IEnumerator OnExit() {
			AppManager.Instance.m_fade.StartFade(new FadeOut() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			SceneManager.LoadScene("StartUp");
			yield return null;

		}
	}
}