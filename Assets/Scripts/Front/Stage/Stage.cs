using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox {
	public class Stage : MonoBehaviour {

		//スタート地点だと判別するid
		private const int START_POINT_ID = 0;

		List<StartPoint> m_startPoints;

		List<GoalPoint> m_goalPoints;

		private bool m_isGoal;

		public void Initialize() {
			m_startPoints = FindObjectsOfType<StartPoint>().ToList();
			m_goalPoints = FindObjectsOfType<GoalPoint>().ToList();
			m_isGoal = false;
		}

		public void UpdateByFrame() {
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
		/// <param name="arg_player"></param>
		public void PlayerGenerate(PlayerComponent arg_player) {
			foreach (StartPoint point in m_startPoints) {
				if (point.m_id == START_POINT_ID) {
					point.m_isActive = true;
					point.Generate(arg_player);
				}
			}
		}

		/// <summary>
		/// プレイヤーがゴールまで達したか
		/// </summary>
		/// <returns></returns>
		public bool DoesPlayerReachGoal() {
			return m_isGoal;
		}
	}
}