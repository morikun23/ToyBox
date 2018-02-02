using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox {
	public class Stage : MonoBehaviour {

		//---------------------------------------------
		//	プレイヤー
		//---------------------------------------------

		///<summary>プレイヤー</summary>
		private Player m_player;

		//---------------------------------------------
		//　ステージ情報
		//---------------------------------------------

		///<summary>初期地点</summary>
		private List<StartPoint> m_startPoints;

		///<summary>ゴール地点</summary>
		private List<GoalPoint> m_goalPoints;

		///<summary>小部屋</summary>
		private List<PlayRoom> m_playRooms;

		///<summary>現在プレイヤーが入っている部屋</summary>
		private PlayRoom m_activePlayroom;

		///<summary>ゴールしたときのコールバック</summary>
		private System.Action<object> m_onGoalAction;

		/// <summary>
		/// ※継承禁止
		/// Initializeの処理を実行しないとエラーになるため
		/// </summary>
		protected void Start() { }

		/// <summary>
		/// ※継承禁止
		/// Initializeの処理を実行しないとエラーになるため
		/// </summary>
		protected void Update() { }

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_player">プレイヤー</param>
		/// <param name="arg_defaultRoomId">プレイさせる小部屋ID</param>
		/// <param name="arg_onGoalAction">ゴール時のコールバック</param>
		public void Initialize(Player arg_player ,uint arg_defaultRoomId , System.Action<object> arg_onGoalAction) {

			if (arg_player == null) {
				Debug.LogError("プレイヤーが見つかりません");
				return;
			}

			if (arg_onGoalAction == null) {
				Debug.LogError("コールバックが設定されていません");
				return;
			}

			m_player = arg_player;
			m_onGoalAction = arg_onGoalAction;

			#region ステージ情報の取得
			m_startPoints = transform.GetComponentsInChildren<StartPoint>().ToList();
			m_goalPoints = transform.GetComponentsInChildren<GoalPoint>().ToList();
			m_playRooms = transform.GetComponentsInChildren<PlayRoom>().ToList();
			
			foreach(GoalPoint goalPoint in m_goalPoints) {
				goalPoint.Initialize(this.OnGoalPoint);
			}
			foreach(PlayRoom playRoom in m_playRooms) {
				playRoom.Initialize(this.OnRoomEnter);
			}

			//初期リスタート地点を設定 
			PlayRoom defaultRoom = m_playRooms.Find(_ => _.Id == arg_defaultRoomId);

			if (defaultRoom == null) {
				Debug.LogError("指定されたIDの部屋が見つかりません:" + "<color=red>" + arg_defaultRoomId + "</color>");
			}

			SwitchActiveRoom(defaultRoom);
			
			#endregion
			
			if (m_activePlayroom == null) {
				Debug.LogError("指定されたIDのスタート地点が見つかりません:" + "<color=red>" + arg_defaultRoomId + "</color>");
			}

			if (CameraPosController.Instance == null) {
				Debug.LogError("カメラ遷移の機能が使用できません");
			}
			CameraPosController.Instance.SetTargetObject(m_player.gameObject);
			
			PlayerGenerate();

			StartCoroutine(OnUpdate());
		}

		/// <summary>
		/// プレイヤーが死亡したときにリスポーン処理を行う
		/// </summary>
		/// <returns></returns>
		private IEnumerator OnUpdate() {

			//他の初期化が終了するまで待機する
			yield return null;

			while (true) {
				
				yield return new WaitWhile(() => m_player.GetCurrentState() != typeof(Player.DeadState));

				#region プレイヤーが死んだときの演出
				//死亡アニメーションが終了するまで待機
				UpdatePlayingRoom (m_activePlayroom.Id);
				Animator playerAnimation = m_player.AnimatorComponent;

				//同フレーム内でアニメーション情報の更新をするには一度アップデートしなければならない(うろ覚え)
				playerAnimation.Update(0);

				yield return new Tsubakit.WaitForAnimation(playerAnimation , 0);

				Fade fade = AppManager.Instance.m_fade;
				fade.StartFade(new FadeOut() , Color.black , 1.0f);
				yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
				m_player.gameObject.SetActive(false);
				//プレイヤーのリスタート
				PlayerGenerate();

				yield return new WaitWhile(() => m_player.GetCurrentState() == typeof(Player.DeadState));

				yield return new WaitForSeconds(0.5f);

				fade.StartFade(new FadeIn() , Color.black , 1.0f);
				yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

				#endregion
			}
		}
		
		/// <summary>
		/// プレイヤーの生成/リスタートなどの処理をおこなう
		/// </summary>
		public void PlayerGenerate() {
			
			//再生成
			m_player.transform.position = m_activePlayroom.RestartPoint.position;
			m_player.Revive();

			m_player.gameObject.SetActive(true);

			//カメラを移動させる
			CameraPosController.Instance.SetTargetAndStart((int)m_activePlayroom.Id);

			UpdatePlayingRoom (m_activePlayroom.Id);
		}

		/// <summary>
		/// 指定された部屋とその周辺をアクティブ化させる
		/// それ以外は非アクティブ状態にしておく
		/// </summary>
		private void SwitchActiveRoom(PlayRoom arg_playRoom) {
			if (arg_playRoom == null) {
				Debug.LogError("部屋がNULLです");
				return;
			}

			m_activePlayroom = arg_playRoom;

			List<uint> m_sideRoomId = m_activePlayroom.GetSideRoomsId();

			foreach (PlayRoom playRoom in m_playRooms) {
				if (playRoom == m_activePlayroom) {
					playRoom.gameObject.SetActive(true);
					continue;
				}
				if (m_sideRoomId.Contains(playRoom.Id)) {
					playRoom.gameObject.SetActive(true);
				}
				else {
					playRoom.gameObject.SetActive(false);
				}
			}
		}

		//-------------------------------------------
		//	以下、コールバック
		//-------------------------------------------

		/// <summary>
		/// 部屋に入ったときの処理
		/// 入ったときにコールバックとして呼ばれる
		/// </summary>
		/// <param name="arg_nextRoom"></param>
		private void OnRoomEnter(PlayRoom arg_nextRoom) {

			if (arg_nextRoom == m_activePlayroom) return;
			UpdatePlayingRoom (m_activePlayroom.Id);
			//スムーズなカメラ遷移を開始させる
			CameraPosController.Instance.SetTargetAndStart((int)arg_nextRoom.Id);
			
			this.SwitchActiveRoom(arg_nextRoom);
		}

		/// <summary>
		/// ゴールしたときの処理
		/// 通過したときにコールバックとして呼ばれる
		/// </summary>
		/// <param name="arg_id">ゴール地点のID</param>
		protected virtual void OnGoalPoint(object arg_id) {
			this.m_onGoalAction(arg_id);
		}

		/// <summary>
		/// 今遊んでいる部屋番号を更新する
		/// </summary>
		public void UpdatePlayingRoom(uint arg_id){
			AppManager.Instance.user.m_temp.m_playingRoomId = arg_id;

			//選択した部屋のデータを取れるようにListを拡張する
			if (AppManager.Instance.user.m_temp.m_dic_room.Count < arg_id) {
				for(int i = 0;i < arg_id - AppManager.Instance.user.m_temp.m_dic_room.Count;i ++){
					AppManager.Instance.user.m_temp.m_dic_room.Add(new Dictionary<string, object>());
				}
				AppManager.Instance.user.m_temp.m_num_roomWaitTime = 0;
			} else {
				//ArrayList list = AppManager.Instance.user.m_temp.m_dic_room [(int)arg_id - 1] ["Time"] as ArrayList;
				AppManager.Instance.user.m_temp.m_num_roomWaitTime = System.Convert.ToSingle(AppManager.Instance.user.m_temp.m_dic_room [(int)arg_id - 1] ["Time"]);
			}
		}
		
	}
}