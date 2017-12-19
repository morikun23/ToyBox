using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class HomeSceneUI : CommonUI {

		[Header("Buttons")]
		[SerializeField]
		UIButton m_stage1;

		[SerializeField]
		UIButton m_stage2;

		private bool m_isButtonPressed;

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			m_stage1.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnStageSelected , (uint)1000));
			m_stage2.Initialize(new ButtonAction(ButtonEventTrigger.OnPress , this.OnStageSelected , (uint)2000));	
		}

		private void OnStageSelected(object arg_stageId) {

			uint id = uint.Parse(arg_stageId.ToString());

			AppManager.Instance.user.m_temp.m_playStageId = id;
			AppManager.Instance.user.m_temp.m_playRoomId = 1;
			m_isButtonPressed = true;
		}

		public bool IsButtonSelected() {
			return m_isButtonPressed;
		}
	}
}