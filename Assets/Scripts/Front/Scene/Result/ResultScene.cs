using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class ResultScene : ToyBox.Scene {

		public override IEnumerator OnEnter() {
			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.white , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
		}

		public override IEnumerator OnUpdate() {

            //AppManager.Instance.m_audioManager.CreateSe("SE_Gimmick_Clear").Play();

            //音でございます
            AudioManager.Instance.QuickPlaySE("SE_Gimmick_Clear");

			while (true) {
				if (Input.GetMouseButtonDown(0)) {
					break;
				}
				yield return null;
			}
		}

		public override IEnumerator OnExit() {

			AppManager.Instance.m_fade.StartFade(new FadeOut() , Color.black , 2.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

			UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
		}
	}
}