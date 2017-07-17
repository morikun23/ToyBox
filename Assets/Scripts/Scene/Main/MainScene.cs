//担当：森田　勝
//概要：メインシーン全体を管理するクラス
//　　　シーン内の処理はこのクラスをトリガーとしてください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class MainScene : Scene {

		public ActorManager m_actorManager { get; private set; }
		public InputManager m_inputManager { get; private set; }
		public CameraController m_cameraController { get; private set; }

		public MainScene() : base("Scene/MainScene") {
			m_actorManager = new GameObject("Actors").AddComponent<ActorManager>();
			m_inputManager = new InputManager();
			m_cameraController = new CameraController();
		}

		public override void OnEnter() {
			base.OnEnter();
			m_actorManager.Initialize();
			m_inputManager.Initialize(this);
			m_cameraController.Initialize(m_actorManager.m_player);
		}

		public override void OnUpdate() {
			base.OnUpdate();
			m_actorManager.UpdateByFrame();
			m_inputManager.UpdateByFrame(this);
			m_cameraController.UpdateByFrame(m_actorManager.m_player);
		}

		public override void OnExit() {
			base.OnExit();
			Destroy(m_actorManager);
		}

	}
}