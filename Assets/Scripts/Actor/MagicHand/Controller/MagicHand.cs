using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Controller {
	public class MagicHand : MonoBehaviour {

		public Logic.MagicHand m_logic { get; private set; }

		public View.MagicHand m_view { get; private set; }

		public void Initalize(Player arg_player) {
			m_logic = new Logic.MagicHand();
			m_view = GetComponent<View.MagicHand>();

			m_logic.Initialize(this);
			m_view.Initialize(m_logic);
		}

		public void UpdateByFrame(Player arg_player) {
			transform.position = arg_player.m_arm.m_logic.GetTopPosition();
			//角度の補正
			transform.eulerAngles = Vector3.forward * (arg_player.m_arm.m_logic.m_currentAngle - 90);

			m_logic.UpdateByFrame(this);
			m_view.UpdateByFrame(m_logic);
		}
	}
}