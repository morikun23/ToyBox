using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox {
	public class HomeScene : ToyBox.Scene {

		[SerializeField]
		HomeSceneUI m_homeSceneUI;

		public override IEnumerator OnEnter() {

			m_homeSceneUI.Initialize();

			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
		}

		public override IEnumerator OnUpdate() {
			yield return new WaitWhile(() => !m_homeSceneUI.IsButtonSelected());
		}

		public override IEnumerator OnExit() {
			AppManager.Instance.m_fade.StartFade(new FadeOut() , Color.black , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			SceneManager.LoadScene("Main");
		}
	}
}