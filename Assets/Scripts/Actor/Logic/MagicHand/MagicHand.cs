using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public class MagicHand {

		//TODO：基底クラス化
		public Vector2 m_position;
		public Vector2 m_scale;

		public Queue<IMagicHandAction> m_currentActions { get; private set; }
		

		public void Initialize(Player arg_player) {
			m_position = arg_player.m_position + Vector2.up;

			m_currentActions = new Queue<IMagicHandAction>();
		}

		public void UpdateByFrame(Player arg_player) {

			m_position = arg_player.m_arm.GetBottomPosition() + arg_player.m_arm.GetTopPosition();

			if (m_currentActions.Count > 0) {
				IMagicHandAction currentAction = m_currentActions.Peek();
				if (currentAction.IsFinished()) {
					ExitAction();
				}
				else {
					currentAction.OnUpdate(this);
				}
			}
		}

		public void AddTask(IMagicHandAction arg_action) {
			m_currentActions.Enqueue(arg_action);
		}

		public void ExitAction() {
			m_currentActions.Dequeue();
		}
	}
}