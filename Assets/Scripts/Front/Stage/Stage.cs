using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ToyBox {
	public class Stage : MonoBehaviour {

		List<StartPoint> m_startPoints;

		List<GoalPoint> m_goalPoints;

		public void Initialize() {
			m_startPoints = FindObjectsOfType<StartPoint>().ToList();
			m_goalPoints = FindObjectsOfType<GoalPoint>().ToList();
		}

		public void UpdateByFrame() {

		}

		public void PlayerGenerate(PlayerComponent arg_player) {
			foreach (StartPoint point in m_startPoints) {
				if (point.GetType() == typeof(StartPoint)) {
					point.Generate(arg_player);
				}
			}
		}
	}
}