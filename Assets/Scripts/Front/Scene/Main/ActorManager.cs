using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class ActorManager : ObjectBase {

		//プレイヤー
		public Player m_player { get; private set; }
		private PlayerController m_playerController;

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			//プレイヤーのプレハブを生成し、参照する
			m_player = FindObjectOfType<Player>();
			if (!m_player) { PlayerGenerate(); }
			m_player.Initialize();
			m_playerController = new PlayerController();
			m_playerController.Initialize();
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame() {
			m_player.UpdateByFrame();
			m_playerController.UpdateByFrame();
		}

		private void PlayerGenerate() {
			m_player = Instantiate(Resources.Load<Player>("Actor/Player/BD_Player") , m_transform);
		}
	}
}