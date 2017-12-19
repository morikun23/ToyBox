using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox {
	public class MainScene : ToyBox.Scene {

		[SerializeField]
		private StageFactory m_stageFactory;

		[SerializeField]
		MainSceneUI m_mainSceneUI;

		private Stage m_stage;
		
		private Player m_player;

		//シーン遷移用のフラグ
		private bool m_isAblesceneTransition;

		private const string PLAYER_PATH = "Contents/Player/Prefabs/BD_Player";

		public override IEnumerator OnEnter() {
			
			if (m_stageFactory == null) {
				Debug.LogError("StageFactoryに参照できません");
				yield break;
			}
			
			uint playStageId = AppManager.Instance.user.m_temp.m_playStageId;
			uint playRoomId = AppManager.Instance.user.m_temp.m_playRoomId;

			m_stage = m_stageFactory.Load(playStageId);

			if(m_stage == null) {
				Debug.LogError("ステージの生成に失敗しました");
				yield break;
			}

			//プレイヤーの生成
			m_player = Instantiate(Resources.Load<GameObject>(PLAYER_PATH)).GetComponent<Player>();
			if (m_player == null) {
				Debug.LogError("Playerの生成に失敗しました");
				yield break;
			}
			m_player.gameObject.name = "Player";
			m_player.Initialize();

			m_stage.Initialize(m_player,playRoomId, this.OnPlayerGoaled);

			if(m_mainSceneUI == null) {
				Debug.LogError("UIを参照できません");
				yield break;
			}

			m_mainSceneUI.Initialize(m_player);

			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

		}

		public override IEnumerator OnUpdate() {
			//フラグが立つまでシーン遷移を実行しない
			yield return new WaitWhile(() => !m_isAblesceneTransition);
		}

		
		public override IEnumerator OnExit() {
			//TODO:今後リザルトシーンではなくホーム画面に移動する
			SceneManager.LoadScene("Result");
			yield return null;
		}

		private void OnPlayerGoaled(object arg_message) {
			//TODO:ユーザー情報にクリアを加える
			m_isAblesceneTransition = true;
		}

		/// <summary>
		/// ゲームを途中で中断し、ホーム画面へ戻る
		/// </summary>
		public void ExitMainGame() {
			//TODO:現在の進行度を保存する
			m_isAblesceneTransition = true;
		}
	}
}