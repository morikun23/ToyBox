using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Controller {
	public class Arm : Playable {

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
			m_logic.UpdateByFrame(this);
			m_view.UpdateByFrame(m_logic);
		}

		/// <summary>
		/// ロジック部にタスクを追加させて
		/// 動きを開始させる
		/// </summary>
		private void StartAction() {

		}

		//-------------------------------------
		//　以下、Playableの実装
		//-------------------------------------

		/// <summary>
		/// アームを伸ばす
		/// </summary>
		public override void Action1() {
			m_logic.SetTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			m_logic.StateTransition(new Logic.ArmLengthen());
		}

		/// <summary>
		/// アームを固定させる
		/// </summary>
		public override void Action2() {
			m_logic.StateTransition(new Logic.ArmFreezeState());
		}

		/// <summary>
		/// アームを縮める
		/// </summary>
		public override void Action3() {
			m_logic.StateTransition(new Logic.ArmShorten());
		}
	}
}