//担当：森田　勝
//概要：メインシーン全体を管理するクラス
//　　　シーン内の処理はこのクラスをトリガーとしてください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class MainScene : Scene {

		private ActorManager m_actorManager { get; set; }

		public MainScene() : base("Scene/MainScene") {
			m_actorManager = new GameObject("Actors").AddComponent<ActorManager>();
		}

		public override void OnEnter() {
			base.OnEnter();
			m_actorManager.Initialize();
		}

		public override void OnUpdate() {
			base.OnUpdate();
			m_actorManager.UpdateByFrame();
		}

		public override void OnExit() {
			base.OnExit();
			Destroy(m_actorManager);
		}

	}
}