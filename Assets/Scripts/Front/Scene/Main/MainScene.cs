//担当：森田　勝
//概要：メインシーン全体を管理するクラス
//　　　シーン内の処理はこのクラスをトリガーとしてください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class MainScene : Scene {

		public override IEnumerator OnEnter() {
			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
		}

		public override IEnumerator OnUpdate() {
			while (true) {
				if (Input.GetKeyDown(KeyCode.Space)) {
					break;
				}
				yield return null;
			}
		}

		public override IEnumerator OnExit() {
			AppManager.Instance.m_fade.StartFade(new FadeOut() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			UnityEngine.SceneManagement.SceneManager.LoadScene("StartUp");
			yield return null;

		}
	}
}