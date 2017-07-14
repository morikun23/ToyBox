using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Controller {
	public class Arm : MonoBehaviour {

		//ロジック部
		public Logic.Arm m_logic { get; private set; }

		//描画部
		private View.Arm m_view { get; set; }

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_player"></param>
		public void Initialize(Controller.Player arg_player) {
			m_logic = new Logic.Arm();
			m_view = GetComponent<View.Arm>();

			m_logic.Initialize(this);
			m_view.Initialize(m_logic);
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void UpdateByFrame(Controller.Player arg_player) {

			if(arg_player.m_logic.m_currentState.GetType() == typeof(Logic.PlayerReach)) {
				if (!this.m_logic.m_isActive) {
					StartAction();
				}
			}

			m_logic.UpdateByFrame(this);
			m_view.UpdateByFrame(m_logic);
		}

		/// <summary>
		/// ロジック部にタスクを追加させて
		/// 動きを開始させる
		/// </summary>
		private void StartAction() {
			m_logic.AddTask(new Logic.ArmShorten());
			m_logic.AddTask(new Logic.ArmFreeze());
			m_logic.AddTask(new Logic.ArmLengthen());
		}
	}
}