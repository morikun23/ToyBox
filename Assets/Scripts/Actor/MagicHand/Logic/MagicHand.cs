//担当：森田　勝
//概要：マジックハンドを制御するクラス
//　　　掴む、放すも含める
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	[System.Serializable]
	public class MagicHand : ActorBase{
		
		public Queue<IMagicHandAction> m_currentActions { get; private set; }
		
		public MagicHand() : base() {
			m_currentActions = new Queue<IMagicHandAction>();
		}

		public void Initialize(Controller.MagicHand arg_controller) {
			m_position = arg_controller.transform.position;
			m_rotation = arg_controller.transform.eulerAngles.z;
			m_currentActions = new Queue<IMagicHandAction>();
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_player">自身を所持しているプレイヤー</param>
		public void UpdateByFrame(Controller.MagicHand arg_controller) {

			//座標の補正
			m_position = arg_controller.transform.position;
			m_rotation = arg_controller.transform.eulerAngles.z;

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
		
		/// <summary>
		/// アームの向きに合わせて角度を調整する
		/// </summary>
		/// <param name="arg_angle">角度</param>
		public void Rotate(float arg_angle) {
			m_rotation = arg_angle;
		}
	}
}