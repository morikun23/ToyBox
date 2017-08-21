using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class ActorManager : MonoBehaviour {

		//プレイヤー
		public Player m_player { get; private set; }

		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			//プレイヤーのプレハブを生成し、参照する
			m_player = FindObjectOfType<Player>();
			m_player.Initialize();
			
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame() {
			m_player.UpdateByFrame();
		}
	}
}