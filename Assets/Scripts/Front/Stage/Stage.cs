using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox {
	public class Stage : MonoBehaviour {

		//初期地点
		private List<StartPoint> m_startPoints;

		//ゴール地点
		private List<GoalPoint> m_goalPoints;

		//小部屋
		private List<PlayRoom> m_playRooms;
		
		//現在のリスタート地点
		private StartPoint m_currentStartPoint;

		//監視を行うプレイヤー
		private Player m_player;

		//現在プレイヤーが入っている部屋
		private PlayRoom m_activePlayroom;

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
		/// <param name="arg_player"></param>
		/// <param name="arg_defaultStartPoint"></param>
		/// <param name="arg_onGoalAction"></param>
		public void Initialize(Player arg_player ,uint arg_defaultStartPoint , System.Action<object> arg_onGoalAction) {
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

			m_startPoints = transform.GetComponentsInChildren<StartPoint>().ToList();
			m_goalPoints = transform.GetComponentsInChildren<GoalPoint>().ToList();
			m_playRooms = transform.GetComponentsInChildren<PlayRoom>().ToList();

			foreach (CheckPoint checkPoint in m_startPoints) {
				checkPoint.Initialize(this.OnCheckPoint);
			}
			foreach(GoalPoint goalPoint in m_goalPoints) {
				goalPoint.Initialize(this.OnGoalPoint);
			}
			foreach(PlayRoom playRoom in m_playRooms) {
				playRoom.Initialize(this.OnRoomEnter);
			}

			//初期リスタート地点を設定 
			m_activePlayroom = m_playRooms.Find(_ => _.Id == arg_defaultStartPoint);

			if(m_activePlayroom == null) {
				Debug.LogError("指定されたIDの部屋が見つかりません:" + "<color=red>" + arg_defaultStartPoint + "</color>");
			}

			m_currentStartPoint = m_startPoints.Find(_ => _.m_id == arg_defaultStartPoint);

			if (m_activePlayroom == null) {
				Debug.LogError("指定されたIDのスタート地点が見つかりません:" + "<color=red>" + arg_defaultStartPoint + "</color>");
			}

			if (CameraPosController.Instance == null) {
				Debug.LogError("カメラ遷移の機能が使用できません");
			}
			CameraPosController.Instance.SetTargetObject(m_player.gameObject);

			SetRoomActive(m_activePlayroom);

			PlayerGenerate(m_currentStartPoint);

			StartCoroutine(OnUpdate());
		}

		private IEnumerator OnUpdate() {
			while (true) {
				
				yield return new WaitWhile(() => m_player.GetCurrentState() != typeof(PlayerDeadState));

				yield return OnPlayerDead();
			}
		}

		private IEnumerator OnPlayerDead() {
			#region プレイヤーが死んだときの演出
			//死亡アニメーションが終了するまで待機
			yield return new Tsubakit.WaitForAnimation(m_player.m_viewer.m_animator , 0);

			Fade fade = AppManager.Instance.m_fade;
			fade.StartFade(new FadeOut() , Color.black , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

			//プレイヤーのリスタート
			PlayerGenerate(m_currentStartPoint);

			fade.StartFade(new FadeIn() , Color.black , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

			#endregion
		}

		/// <summary>
		/// プレイヤーの生成/リスタートなどの処理をおこなう
		/// </summary>
		/// <param name="arg_startPoint">指定されたスタート地点に生成します</param>
		public void PlayerGenerate(StartPoint arg_startPoint) {

			if (arg_startPoint == null) {
				Debug.LogError("チェックポイントがNULLです");
				return;
			}
			
			//スタート地点が配置されている部屋をアクティブ化
			m_activePlayroom = m_playRooms.Find(_ => _.Id == arg_startPoint.m_id);
			if(m_activePlayroom == null) {
				Debug.LogError("復活先の部屋が見つかりませんでした");
			}

			SetRoomActive(m_activePlayroom);
			
			//再生成
			arg_startPoint.Generate(m_player);
			m_player.Revive();

			//カメラを移動させる
			CameraPosController.Instance.SetTargetAndStart((int)m_activePlayroom.Id);
		}


		/// <summary>
		/// 部屋に入ったときの処理
		/// 入ったときにコールバックとして呼ばれる
		/// </summary>
		/// <param name="arg_nextRoom"></param>
		private void OnRoomEnter(PlayRoom arg_nextRoom) {

			if (arg_nextRoom == m_activePlayroom) return;

			//スムーズなカメラ遷移を開始させる
			CameraPosController.Instance.SetTargetAndStart((int)arg_nextRoom.Id);

			m_activePlayroom = arg_nextRoom;
			this.SetRoomActive(m_activePlayroom);
			
		}

		/// <summary>
		/// 指定された部屋とその周辺をアクティブ化させる
		/// それ以外は非アクティブ状態にしておく
		/// </summary>
		private void SetRoomActive(PlayRoom arg_playRoom) {
			if(arg_playRoom == null) {
				Debug.LogError("部屋がNULLです");
				return;
			}

			List<uint> m_sideRoomId = arg_playRoom.GetSideRoomsId();

			foreach(PlayRoom playRoom in m_playRooms) {
				if (playRoom == arg_playRoom) {
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

		/// <summary>
		/// チェックポイントを通過したときの処理
		/// 通過したときにコールバックとして呼ばれる
		/// </summary>
		/// <param name="arg_startPoint"></param>
		protected virtual void OnCheckPoint(object arg_startPoint) {

			StartPoint startPoint = (StartPoint)arg_startPoint;

			if (arg_startPoint == null) {
				Debug.LogError("チェックポイントがNULLです");
				return;
			}
			if (startPoint.m_isActive) return;

			//リスタート地点を更新
			this.m_currentStartPoint = startPoint;
		}

		/// <summary>
		/// ゴールしたときの処理
		/// 通過したときにコールバックとして呼ばれる
		/// </summary>
		/// <param name="arg_id">ゴール地点のID</param>
		protected virtual void OnGoalPoint(object arg_id) {
			this.m_onGoalAction(arg_id);
		}


	}
}