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
		private Queue<IArmAction> m_tasks;
		
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_player">自身を所持しているプレイヤー</param>
		public void Initialize(Player arg_player) {
			m_position = arg_player.m_position + new Vector2(0,0.3f);
			m_rotation = 0f;
	
			m_currentAngle = 90;
			m_currentRange = 5f;
			m_currentLength = 0f;
			m_tasks = new Queue<IArmAction>();
			m_isActive = true;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_player">自身を所持しているプレイヤー</param>
		public void UpdateByFrame(Player arg_player) {
			m_position = arg_player.m_position + new Vector2(0,0.3f);
			
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

		public void AddTask(IArmAction arg_armAction) {
			m_tasks.Enqueue(arg_armAction);
		}
	}
}