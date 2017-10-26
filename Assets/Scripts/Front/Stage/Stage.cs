using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox {

	public class StageConfig {
		//ゴール地点のID幅
		public const int GOAL_POINT_ID_MIN = 10;
		public const int GOAL_POINT_ID_MAX = 99;
		//スタート地点のID幅
		public const int START_POINT_ID_MIN = 0100;
		public const int START_POINT_ID_MAX = 9999;
	}

	public class Stage : MonoBehaviour {
		
		//中間地点
		private List<CheckPoint> m_checkPoints;

		//小部屋
		private List<PlayRoom> m_playRooms;

		//部屋の遷移を通知するコライダー
		private List<RoomCollider> m_roomColliders;

		//初期スタート地点だと判別するid
		private const int START_POINT_ID = 9999;

		//現在のリスタート地点
		private StartPoint m_currentStartPoint;

		//ゴールしたか
		private bool m_isGoal;

		//監視を行うプレイヤー
		private PlayerComponent m_player;

		//現在プレイヤーが入っている部屋
		private PlayRoom m_activePlayroom;
		
		private void Start() {
			m_checkPoints = transform.GetComponentsInChildren<CheckPoint>().ToList();
			m_playRooms = transform.GetComponentsInChildren<PlayRoom>().ToList();
			m_roomColliders = transform.GetComponentsInChildren<RoomCollider>().ToList();

			foreach(CheckPoint checkPoint in m_checkPoints) { checkPoint.Initialize(this.OnCheckPoint); }
			foreach(RoomCollider roomCollider in m_roomColliders) { roomCollider.Initialize(this.OnRoomEnter); }

			//初期リスタート地点を設定
			m_currentStartPoint = m_checkPoints.Find(_ => _.m_id == START_POINT_ID) as StartPoint;
			m_activePlayroom = m_playRooms.Find(_ => _.Id == 1);

			SetRoomActive(m_activePlayroom.Id);
		}

		private void Update() {

			if(m_player.GetCurrentState() == typeof(PlayerDeadState)) {
				//プレイヤーが死亡状態である
				PlayerGenerate(m_currentStartPoint);
			}
			
		}

		/// <summary>
		/// プレイヤーの生成を行う
		/// ここに生成時の演出を加えてもいい
		/// </summary>
		/// <param name="arg_startPoint">指定されたスタート地点に生成します</param>
		public void PlayerGenerate(StartPoint arg_startPoint) {
			if (arg_startPoint == null)	return;
			if (!arg_startPoint.m_isActive) return;

			#region IDを識別するための処理（複雑なので隠蔽させます）
			System.Func<int , int> RoomId = (id) => {
				//IDの桁数を調べる(1の場合は0)
				int digitCount = id / 10;

				//調べる桁の位置(3桁なら3桁目のみ、4桁なら3桁目と4桁目、5桁なら3桁目~5桁目)
				int digitPlace = 2 + (digitCount - 3);

				string stId = string.Empty;

				int i = digitCount;

				while(i >= 2) {
					//数値を結合させるため一旦文字列へ
					int temp = (id / (int)Mathf.Pow(10 , i) % 10);
					stId += temp.ToString();
					i -= 1;
				}
				return int.Parse(stId);
			};
			#endregion

			//スタート地点が配置されている部屋をアクティブ化
			m_activePlayroom = m_playRooms.Find(_ => _.Id == RoomId(arg_startPoint.m_id));
			SetRoomActive(m_activePlayroom.Id);
			
			//再生成
			arg_startPoint.Generate(m_player);
			m_player.Revive();

			//カメラを移動させる
			CameraPosController.Instance.SetTargetAndStart(m_activePlayroom.Id);
		}

		/// <summary>
		/// プレイヤーがゴールまで達したか
		/// </summary>
		/// <returns></returns>
		public bool DoesPlayerReachGoal() {
			return m_isGoal;
		}

		/// <summary>
		/// 監視を行うプレイヤーを設定する
		/// </summary>
		public void SetPlayer(PlayerComponent arg_player) {
			if(arg_player == null) { return; }
			m_player = arg_player;
		}

		/// <summary>
		/// コライダーからのコールバック
		/// </summary>
		/// <param name="roomManager"></param>
		private void OnRoomEnter(int arg_prevId , int arg_nextId) {

			int currentId = m_activePlayroom.Id;

			if (arg_nextId == currentId) return;

			//スムーズなカメラ遷移を開始させる
			CameraPosController.Instance.SetTargetAndStart(arg_nextId);
			PlayRoom nextRoom = m_playRooms.Find(_ => _.Id == arg_nextId);

			if (nextRoom != null) {
				this.SetRoomActive(nextRoom.Id);
				m_activePlayroom = nextRoom;
			}
		}

		/// <summary>
		/// 指定された部屋番号の前後をアクティブ化させる
		/// それ以外は非アクティブ状態にしておく
		/// </summary>
		private void SetRoomActive(int arg_id) {
			foreach(PlayRoom playRoom in m_playRooms) {
				if(arg_id - 1 <= playRoom.Id && playRoom.Id <= arg_id + 1) {
					playRoom.gameObject.SetActive(true);
				}
				else { playRoom.gameObject.SetActive(false); }
			}
		}


		/// <summary>
		/// チェックポイントからのコールバックを受け取る
		/// </summary>
		/// <param name="id"></param>
		private void OnCheckPoint(int arg_id) {
			
			//渡された値が２つの値の間であるかを調べる
			System.Func<int,int,int,bool> Between = (num , min , max) => {
				return (min <= num && num < max);
			};

			if (Between(arg_id , StageConfig.START_POINT_ID_MIN, StageConfig.START_POINT_ID_MAX)) {
				//渡されたIDが中間地点であったら、中間地点を更新する
				StartPoint startPoint = m_checkPoints.Find(_ => _.m_id == arg_id) as StartPoint;
				if (startPoint) {
					if(!startPoint.m_isActive) m_currentStartPoint = startPoint;
				}
			}
			else if (Between(arg_id,StageConfig.GOAL_POINT_ID_MIN,StageConfig.GOAL_POINT_ID_MAX)) {
				//渡されたIDがゴール地点であったら、ゴールしたことを通知する
				//TODO:複数ゴールの対応
				m_isGoal = true;
			}
			else {
				Debug.LogError("認識されていないIDを検出");
			}
		}
	}
}