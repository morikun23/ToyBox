//担当：森田　勝
//概要：プレイヤーの腕を制御するクラス
//　　　伸び縮みしたり、当たり判定があったり
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	[System.Serializable]
	public class Arm : ActorBase {
		
		//最大の射程
		private const float MAX_RANGE = 10f;

		//現在の射程（射程は変更可能）
		public float m_currentRange { get; private set; }

		//現在の射出角度
		public float m_currentAngle;

		//現在の射出距離
		public float m_currentLength;

		//アームのタスク
		private Stack<IArmAction> m_tasks;

		//タスク実行中か
		public bool m_isActive { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// 必要なメモリを取得
		/// </summary>
		public Arm() {
			m_tasks = new Stack<IArmAction>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_controller">コントローラー部</param>
		public void Initialize(Controller.Arm arg_controller) {
			//プレイヤーの座標に依存する
			m_position = arg_controller.transform.position;
			m_rotation = arg_controller.transform.eulerAngles.z;
	
			m_currentAngle = 90;
			m_currentRange = 5f;
			m_currentLength = 0f;
			m_isActive = true;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_controller">コントローラー部</param>
		public void UpdateByFrame(Controller.Arm arg_controller) {

			//プレイヤーの座標に依存する
			m_position = arg_controller.transform.position;

			//タスクが残っていれば実行する
			if (m_tasks.Count > 0) {
				m_isActive = true;
				IArmAction currentTask = m_tasks.Peek();
				currentTask.OnUpdate(this);
			}
			else {
				m_isActive = true;
			}
		}
		
		/// <summary>
		/// アームを回転させる
		/// </summary>
		/// <param name="arg_angle">角度(度数)</param>
		public void Rotate(float arg_angle) {
			m_currentAngle += arg_angle;
			m_currentAngle = Mathf.Repeat(m_currentAngle , 359);
		}

		/// <summary>
		/// アームの先端の座標を取得する
		/// </summary>
		/// <returns>※絶対座標</returns>
		public Vector2 GetTopPosition() {
			Vector2 pos = new Vector2(Mathf.Cos(Mathf.Deg2Rad * m_currentAngle),Mathf.Sin(Mathf.Deg2Rad * m_currentAngle)).normalized;
			pos *= m_currentLength;
			return this.GetBottomPosition() + pos;
		}

		/// <summary>
		/// アームの根元の座標を取得する
		/// </summary>
		/// <returns>※絶対座標</returns>
		public Vector2 GetBottomPosition() {
			return m_position;
		}

		/// <summary>
		/// タスク追加
		/// </summary>
		/// <param name="arg_armAction">追加するタスク</param>
		public void AddTask(IArmAction arg_armAction) {
			m_tasks.Push(arg_armAction);
		}

		/// <summary>
		/// 現在のタスクを終了させる
		/// </summary>
		public void FinishCurrentTask() {
			m_tasks.Pop();
		}
	}
}