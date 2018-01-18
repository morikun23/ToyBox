using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox {
	public class MainScene : ToyBox.Scene {

		//-------------------------------------------
			[Header("Player")]
		//-------------------------------------------

		[SerializeField]
		private GameObject m_playerPrefab;

		private Player m_player;

		//-------------------------------------------
			[Header("Buttons")]
		//-------------------------------------------

		[SerializeField]
		UIDraggableButton m_armButton;

		[SerializeField]
		UIButton m_leftButton;

		[SerializeField]
		UIButton m_rightButton;

		[SerializeField]
		UIButton m_optionButton;

		//-------------------------------------------
			[Header("Stage")]
		//-------------------------------------------
		[SerializeField]
		private StageFactory m_stageFactory;
		
		private Stage m_stage;
		
		//シーン遷移用のフラグ
		private bool m_isAbleSceneTransition;

		private bool m_isEnableInput = true;

		private bool m_isReachMode;

		[SerializeField]
		private LineRenderer line;

		//-------------------------------------------
		//	デバッグ機能
		//-------------------------------------------

#if UNITY_EDITOR
		[System.Serializable]
		private class DebugInfo {

			[Tooltip("デバッグモードを有効にするか")]
			public bool m_isDebugMode;

			[Tooltip("旧射出モードを利用するか")]
			public bool m_useAnotherReachMode;


			public bool UseAnotherReachMode {
				get {
					return m_isDebugMode && m_useAnotherReachMode;
				}
			}
		}

		[SerializeField]
		private DebugInfo m_debugInfo;

#endif
		public override IEnumerator OnEnter() {

			if (m_stageFactory == null) {
				Debug.LogError("StageFactoryに参照できません");
				yield break;
			}

			uint playStageId = 1000;//AppManager.Instance.user.m_temp.m_playStageId;
			uint playRoomId = 1;//AppManager.Instance.user.m_temp.m_playRoomId;

			m_stage = m_stageFactory.Load(playStageId);

			if(m_stage == null) {
				Debug.LogError("ステージの生成に失敗しました");
				yield break;
			}

			//プレイヤーの生成
			m_player = Instantiate(m_playerPrefab).GetComponent<Player>();
			if (m_player == null) {
				Debug.LogError("Playerの生成に失敗しました");
				yield break;
			}
			m_player.gameObject.name = "Player";

			m_stage.Initialize(m_player,playRoomId, this.OnPlayerGoaled);
			
			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 1.0f);


			AudioManager.Instance.StopBGM();
			if ((int)playStageId == 1000) {
				AudioManager.Instance.RegisterBGM("BGM_Stage1");
			}
			else if((int)playStageId == 2000) {
				AudioManager.Instance.RegisterBGM("BGM_Stage2");
			}
			AudioManager.Instance.PlayBGM(1.5f);

			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

			m_leftButton.Initialize(
				new ButtonAction(ButtonEventTrigger.OnPress , this.OnMoveButtonDown , (int)Player.Direction.LEFT) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp , (int)Player.Direction.LEFT)
			);

			m_rightButton.Initialize(
				new ButtonAction(ButtonEventTrigger.OnPress , this.OnMoveButtonDown , (int)Player.Direction.RIGHT) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp , (int)Player.Direction.RIGHT)
			);

			m_armButton.Initialize(
				new ButtonAction(ButtonEventTrigger.OnPress , this.OnArmButtonDown) ,
				new ButtonAction(ButtonEventTrigger.OnSwipe , this.OnArmButtonStay) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnArmButtonUp)
			);
		}

		public override IEnumerator OnUpdate() {
			//フラグが立つまでシーン遷移を実行しない
			yield return new WaitWhile(() => !m_isAbleSceneTransition);
		}
		
		public override IEnumerator OnExit() {
			//TODO:今後リザルトシーンではなくホーム画面に移動する
			AppManager.Instance.m_fade.StartFade(new FadeOut() , Color.white , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			AudioManager.Instance.StopBGM();
			AudioManager.Instance.StopSE("Foot");
			AudioManager.Instance.ReleaseSE("Foot");
			SceneManager.LoadScene("Result");
		}

		private void OnPlayerGoaled(object arg_message) {
			//TODO:ユーザー情報にクリアを加える
			m_isAbleSceneTransition = true;
		}

		/// <summary>
		/// ゲームを途中で中断し、ホーム画面へ戻る
		/// </summary>
		public void ExitMainGame() {
			//TODO:現在の進行度を保存する
			m_isAbleSceneTransition = true;
		}
		
		//-------------------------------------------------------
		//　以下、ボタン入力のコールバック
		//-------------------------------------------------------

		/// <summary>
		/// 移動ボタンが押されたときの処理
		/// </summary>
		/// <param name="arg_object"></param>
		private void OnMoveButtonDown(object arg_object) {
			if (!m_isEnableInput) return;

			int direction = (int)arg_object;
			
			switch (direction) {
				case (int)Player.Direction.LEFT:	m_player.Run(Player.Direction.LEFT);	break;
				case (int)Player.Direction.RIGHT:	m_player.Run(Player.Direction.RIGHT);	break;
				default: Debug.LogError("ボタンからのコールバックが正しくありません");			break;
			}
		}
	
		/// <summary>
		/// 移動ボタンが離されたときの処理
		/// </summary>
		/// <param name="arg_object"></param>
		private void OnMoveButtonUp(object arg_object) {
			if (!m_isEnableInput) return;

			int direction = (int)arg_object;

			switch (direction) {
				case (int)Player.Direction.LEFT: m_player.Stop(Player.Direction.LEFT);		break;
				case (int)Player.Direction.RIGHT: m_player.Stop(Player.Direction.RIGHT);	break;
				default: Debug.LogError("ボタンからのコールバックが正しくありません");			break;
			}
		}

		/// <summary>
		/// 射出ボタンが押されたときの処理
		/// </summary>
		private void OnArmButtonDown() {
			if (m_player.PlayableHand.IsGrasping) {
				if (m_player.PlayableHand.GraspingItem.IsAbleRelease()) {
					m_player.PlayableHand.Release();
				}
			}
			else {
				m_isReachMode = true;
			}
		}

		/// <summary>
		/// 射出ボタンが押されている間の処理
		/// </summary>
		private void OnArmButtonStay() {
			if (!m_isReachMode) return;
			if (m_armButton.Direction == Vector2.zero) {
				line.enabled = false;
			}
			else {
				line.enabled = true;
				line.SetPosition(0 , m_player.PlayableArm.BottomPosition);
				line.SetPosition(1 , m_player.PlayableArm.BottomPosition + (m_armButton.Direction * m_player.PlayableArm.Range));
			}
		}

		/// <summary>
		/// 射出ボタンが離されたときの処理
		/// </summary>
		private void OnArmButtonUp() {
			if (!m_isReachMode) return;
			if (m_armButton.Direction != Vector2.zero) {
				m_player.PlayableArm.ReachOut(m_armButton.Direction);
				//m_reachDirectionBuf = Vector2.zero;
			}
			line.enabled = false;
		}
	}
}