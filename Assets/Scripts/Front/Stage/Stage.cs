using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox {
	public class Stage : MonoBehaviour {

		//スタート地点だと判別するid
		private const int START_POINT_ID = 0;

		//現在のリスタート地点
		private StartPoint m_currentStartPoint;

		//ゴール地点（複数ゴールに対応）
		List<GoalPoint> m_goalPoints;

		//ゴールしたか
		private bool m_isGoal;

		//監視を行うプレイヤー
		private PlayerComponent m_player;

		private void Start() {
			m_goalPoints = FindObjectsOfType<GoalPoint>().ToList();
			m_isGoal = false;
		}

		private void Update() {

			if(m_player.GetCurrentState() == typeof(PlayerDeadState)) {
				//プレイヤーが死亡状態である
				PlayerGenerate(m_currentStartPoint);
			}

			foreach(GoalPoint goal in m_goalPoints) {
				if (goal.m_isActive) {
					m_isGoal = true;
				}
			}
		}

		/// <summary>
		/// プレイヤーの生成を行う
		/// ここに生成時の演出を加えてもいい
		/// </summary>
		/// <param name="arg_startPoint">指定されたスタート地点に生成します</param>
		public void PlayerGenerate(StartPoint arg_startPoint) {
			if (arg_startPoint)	return;

			arg_startPoint.Generate(m_player);
			m_player.Revive();
			
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
			m_player = arg_player;
		}

		/// <summary>
		/// リスタート位置を設定する
		/// </summary>
		public void SetActiveStartPoint(StartPoint arg_startPoint) {
			if(arg_startPoint == null) { return; }
			m_currentStartPoint = arg_startPoint;
		}
	}
}