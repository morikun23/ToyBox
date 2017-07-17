//担当：森田　勝
//概要：ゲーム内のプレイアブルキャラクター
//　　　移動などのアクションはUIなどから
//　　　マジックハンドの制御は自身がトリガーとなる
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	[System.Serializable]
	public class Player : CharacterBase {

		//自身の状態（Stateパターンでの実装）
		public IPlayerState m_currentState { get; private set; }
		
		//タスク
		public Queue<IPlayerCommand> m_task { get; private set; }

		//地面に接しているか
		public bool m_isGrounded { get; private set; }

		/// <summary>
		/// 必要なメモリを確保
		/// </summary>
		public Player() : base(){
			m_task = new Queue<IPlayerCommand>();
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize(Controller.Player arg_player) {
			m_position = arg_player.transform.position;
			m_rotation = arg_player.transform.eulerAngles.z;
			m_currentState = new PlayerIdleState();
			m_speed = 0.1f;
			m_direction = Direction.RIGHT;
			m_isGrounded = arg_player.IsGrounded();
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame(Controller.Player arg_player){
			//Rigidbodyの影響で調整された座標など合わせる
			m_position = arg_player.transform.position;
			m_rotation = arg_player.transform.eulerAngles.z;

			m_isGrounded = arg_player.IsGrounded();

			if (m_currentState != null) {
				m_currentState.OnUpdate(this);
			}

		}
		
		/// <summary>
		/// 状態を遷移させる
		/// </summary>
		/// <param name="arg_nextState">次の状態</param>
		public void StateTransition(IPlayerState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}
		
		/// <summary>
		/// タスクを追加する
		/// </summary>
		/// <param name="arg_task"></param>
		public void AddTask(IPlayerCommand arg_task) {
			m_currentState.AddTask(this,arg_task);
		}
		
	}
}