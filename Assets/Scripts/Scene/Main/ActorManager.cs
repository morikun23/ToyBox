using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Main {
	public class ActorManager : MonoBehaviour {

		//プレイヤー
		public Controller.Player m_player { get; set; }
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {
			//プレイヤーのプレハブを生成し、参照する
			m_player = Instantiate
				(Resources.Load<GameObject>("Actor/Player/BD_Player"),this.transform)
				.GetComponent<Controller.Player>();
			m_player.transform.position = new Vector3(-6 , 0);
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