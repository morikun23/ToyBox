using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class StartUpScene : ToyBox.Scene {

		public override IEnumerator OnEnter() {
			
			yield break;
		}

		public override IEnumerator OnUpdate() {
			yield break;
		}

		public override IEnumerator OnExit() {
			yield return new WaitUntil(()=>AppManager.Instance.NCMB.IsAbleNCMBWrrite());

			AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
			yield return null;

		}
	}
}