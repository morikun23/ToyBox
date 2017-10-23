using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class StartUpScene : ToyBox.Scene {

		public override IEnumerator OnEnter() {
			AppManager.Instance.m_fade.Fill(Color.black);
			yield break;
		}

		public override IEnumerator OnUpdate() {
			yield break;
		}

		public override IEnumerator OnExit() {
			UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
			yield return null;

		}
	}
}