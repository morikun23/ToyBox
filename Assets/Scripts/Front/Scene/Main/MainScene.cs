using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox {
	public class MainScene : ToyBox.Scene , IArmCallBackReceiver , IHandCallBackReceiver {

		//-------------------------------------------
			[Header("Player")]
		//-------------------------------------------

		///<summary>生成するプレイヤーのプレハブ</summary>
		[SerializeField]
		private GameObject m_playerPrefab;

		/// <summary>生成されたプレイヤー</summary>
		private Player m_player;

		/// <summary>射出モード時の予測線</summary>
		[SerializeField]
		private LineRenderer m_armReachLine;

		/// <summary>射出モードフラグ</summary>
		private bool m_isReachMode;

		//-------------------------------------------
			[Header("Buttons")]
		//-------------------------------------------

		///<summary>射出ボタン（エイム時）</summary>
		[SerializeField]
		UIDraggableButton m_armReachButton;

		///<summar>射出ボタン（アイテム所持時）</summary>
		[SerializeField]
		UIButton m_armReleaseButton;

		/// <summary>左移動ボタン</summary>
		[SerializeField]
		UIButton m_leftButton;

		/// <summary>右移動ボタン</summary>
		[SerializeField]
		UIButton m_rightButton;

		/// <summary>オプションボタン</summary>
		[SerializeField]
		UIButton m_optionButton;


		//-------------------------------------------
			[Header("Stage")]
		//-------------------------------------------

		/// <summary>ステージ生成クラス</summary>
		[SerializeField]
		private StageFactory m_stageFactory;

		/// <summary>生成されたステージ</summary>
		private Stage m_stage;

		/// <summary>シーン遷移フラグ</summary>
		private bool m_isAbleSceneTransition;

		//-------------------------------------------
		//	デバッグ機能
		//-------------------------------------------
		#region デバッグ機能
#if UNITY_EDITOR
		[System.Serializable]
		private class DebugInfo {

			[Tooltip("デバッグモードを有効にするか")]
			public bool m_isDebugMode;

			[Tooltip("旧射出モードを利用するか")]
			public bool m_useAnotherReachMode;

			public uint m_usingStageId = 1000;

			public uint m_usingRoomId = 1;

			public bool UseAnotherReachMode {
				get {
					return m_isDebugMode && m_useAnotherReachMode;
				}
			}
		}

		[SerializeField]
		private DebugInfo m_debugInfo;

#endif
		#endregion デバッグ機能

		/// <summary>
		/// シーン開始時
		/// </summary>
		/// <returns></returns>
		public override IEnumerator OnEnter() {

			if (m_stageFactory == null) {
				Debug.LogError("StageFactoryに参照できません");
				yield break;
			}

			uint playStageId = AppManager.Instance.user.m_temp.m_playStageId;
			uint playRoomId = AppManager.Instance.user.m_temp.m_playRoomId;

#if UNITY_EDITOR
			if (m_debugInfo.m_isDebugMode) {
				playStageId = m_debugInfo.m_usingStageId;
				playRoomId = m_debugInfo.m_usingRoomId;
			}
#endif

			#region プレイヤーとステージをそれぞれ生成

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

			m_player.PlayableArm.AddCallBackReceiver(this);
			m_player.PlayableHand.AddCallBackReceiver(this);

			#endregion

			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 1.0f);

			#region BGMの切り替え
			AudioManager.Instance.StopBGM();
			if ((int)playStageId == 1000) {
				AudioManager.Instance.RegisterBGM("BGM_Stage1");
			}
			else if((int)playStageId == 2000) {
				AudioManager.Instance.RegisterBGM("BGM_Stage2");
			}

			AudioManager.Instance.PlayBGM(1.5f);

			#endregion

			#region ボタンの初期化

			m_leftButton.Initialize(
				new ButtonAction(ButtonEventTrigger.OnLongPress , this.OnMoveButtonDown , (int)Player.Direction.LEFT) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp , (int)Player.Direction.LEFT)
			);

			m_rightButton.Initialize(
				new ButtonAction(ButtonEventTrigger.OnLongPress , this.OnMoveButtonDown , (int)Player.Direction.RIGHT) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnMoveButtonUp , (int)Player.Direction.RIGHT)
			);

			m_armReachButton.Initialize(
				new ButtonAction(ButtonEventTrigger.OnPress , this.OnArmReachButtonDown) ,
				new ButtonAction(ButtonEventTrigger.OnSwipe , this.OnArmReachButtonStay) ,
				new ButtonAction(ButtonEventTrigger.OnRelease , this.OnArmReachButtonUp)
			);

			m_armReleaseButton.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease , this.OnArmReleaseButton));

			m_armReleaseButton.gameObject.SetActive(false);

			#endregion

			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
		}

		/// <summary>
		/// アップデート処理
		/// </summary>
		/// <returns></returns>
		public override IEnumerator OnUpdate() {
			//フラグが立つまでシーン遷移を実行しない
			yield return new WaitWhile(() => !m_isAbleSceneTransition);
		}
		
		/// <summary>
		/// シーン終了
		/// </summary>
		/// <returns></returns>
		public override IEnumerator OnExit() {
			AppManager.Instance.m_fade.StartFade(new FadeOut() , Color.white , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			AudioManager.Instance.StopBGM();
			AudioManager.Instance.StopSE("Foot");
			AudioManager.Instance.ReleaseSE("Foot");

			//TODO:今後リザルトシーンではなくホーム画面に移動する
			SceneManager.LoadScene("Result");
		}

		/// <summary>
		/// プレイヤーがゴールしたときのコールバック
		/// </summary>
		/// <param name="arg_message"></param>
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
			if (!AppManager.Instance.user.m_temp.m_isTouchUI ||
				m_player.PlayableArm.IsUsing()) return;

			if (m_player.GetCurrentState() == typeof(Player.ReachState)) return;

			Item item = m_player.PlayableHand.GraspingItem;
			if(item != null) {
				if(item.Reaction == Item.GraspedReaction.PULL_TO_ITEM) {
					return;
				}
			}

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
			if (!AppManager.Instance.user.m_temp.m_isTouchUI ||
				m_player.PlayableArm.IsUsing()) return;

			int direction = (int)arg_object;

			switch (direction) {
				case (int)Player.Direction.LEFT: m_player.Stop(Player.Direction.LEFT);		break;
				case (int)Player.Direction.RIGHT: m_player.Stop(Player.Direction.RIGHT);	break;
				default: Debug.LogError("ボタンからのコールバックが正しくありません");			break;
			}
		}

		/// <summary>
		/// 射出ボタンが押されたときの処理
		/// 射出モードを開始する
		/// </summary>
		private void OnArmReachButtonDown() {
			if (!AppManager.Instance.user.m_temp.m_isTouchUI ||
				m_player.PlayableArm.IsUsing()) return;
			m_isReachMode = true;
		}

		/// <summary>
		/// 射出ボタンが押されている間の処理
		/// 射出角度を決定する
		/// </summary>
		private void OnArmReachButtonStay() {

			if (!AppManager.Instance.user.m_temp.m_isTouchUI ||
				m_player.PlayableArm.IsUsing()) return;
			if (!m_isReachMode) return;
			if (m_armReachLine == null) return;

			if (m_armReachButton.Direction == Vector2.zero) {
				m_armReachLine.gameObject.SetActive(false);
			}
			else {
				m_armReachLine.gameObject.SetActive(true);

				//始点の描画
				m_armReachLine.SetPosition(0 , (Vector3)m_player.PlayableArm.BottomPosition + Vector3.back * 5f);

				#region 着弾点の取得
				RaycastHit2D hitInfo = Physics2D.Raycast(m_player.PlayableArm.BottomPosition , m_armReachButton.Direction , m_player.PlayableArm.Range , 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Item"));

				if (hitInfo) {
					m_armReachLine.transform.position = hitInfo.point;
				}
				else {
					m_armReachLine.transform.position = (Vector3)m_player.PlayableArm.BottomPosition + (Vector3)(m_armReachButton.Direction * m_player.PlayableArm.Range);
				}
				#endregion

				//終点の描画
				m_armReachLine.SetPosition(1 , m_armReachLine.transform.position + Vector3.back * 5f);
			}
		}

		/// <summary>
		/// 射出ボタンが離されたときの処理
		/// アームを実際に伸ばす
		/// </summary>
		private void OnArmReachButtonUp() {

			if (!AppManager.Instance.user.m_temp.m_isTouchUI ||
				m_player.PlayableArm.IsUsing()) return;
			if (!m_isReachMode) return;

			//不整合のためボタンが傾いていない場合はアームを伸ばさない
			if (m_armReachButton.Direction != Vector2.zero) {
				if (!m_player.PlayableArm.IsUsing()) {
					m_player.ReachOut(m_armReachButton.Direction);
				}
			}

			//射出モード解除
			m_isReachMode = false;
			if (m_armReachLine != null) {
				m_armReachLine.gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// 射出ボタン（アイテム所持中）が押された時の処理
		/// 所持しているアイテムを離す
		/// </summary>
		private void OnArmReleaseButton() {

			if (!AppManager.Instance.user.m_temp.m_isTouchUI) return;

			if (m_player.PlayableHand.IsGrasping) {
				if (m_player.PlayableHand.GraspingItem.IsAbleRelease()) {
					m_player.PlayableHand.Release();
				}
			}
		}

		//----------------------------------------
		//	アームからのコールバック
		//----------------------------------------
		#region アームからのコールバック（時間停止用）

		void IArmCallBackReceiver.OnStartLengthen(Arm arg_arm) {
			//AppManager.Instance.m_timeManager.Pause();
		}

		void IArmCallBackReceiver.OnEndShorten(Arm arg_arm) {
			//AppManager.Instance.m_timeManager.Resume();
		}

		#endregion
		//----------------------------------------
		//	ハンドからのコールバック
		//----------------------------------------
		#region ハンドからのコールバック（ボタン切り替え用）
		void IHandCallBackReceiver.OnCollided(Hand arg_hand) {
			return;
		}

		void IHandCallBackReceiver.OnGrasped(Hand arg_hand) {
			m_armReleaseButton.gameObject.SetActive(true);
			m_armReachButton.gameObject.SetActive(false);
		}

		void IHandCallBackReceiver.OnReleased(Hand arg_hand) {
			m_armReleaseButton.gameObject.SetActive(false);
			m_armReachButton.gameObject.SetActive(true);
		}
		#endregion

	}
}