//担当：森田　勝
//概要：メインシーン全体を管理するクラス
//　　　シーン内の処理はこのクラスをトリガーとしてください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox.Main {
	public class MainScene : ToyBox.Scene {

		private Stage m_stage;

		public override IEnumerator OnEnter() {
			//ステージの生成
			m_stage = FindObjectOfType<Stage>();
			m_stage.Initialize();
			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			
		}

		public override IEnumerator OnUpdate() {
			while (true) {
				m_stage.UpdateByFrame();
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