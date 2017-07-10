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
		
		/// <summary>
		/// 必要なメモリを確保
		/// </summary>
		public Player() : base(){ }

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize(Controller.Player arg_player) {
			m_currentState = new PlayerIdle();
			m_position = arg_player.transform.position;
			m_rotation = arg_player.transform.eulerAngles.z;
			m_speed = 0.5f;
			m_direction = Direction.LEFT;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame(Controller.Player arg_player){
			
			m_position = arg_player.transform.position;
			m_rotation = arg_player.transform.eulerAngles.z;

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
	}
}