using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class CameraController : MonoBehaviour {

		private Camera m_mainCamera;

		public void Initialize(Playable arg_player) {
			//Scene上のCameraを取得する
			m_mainCamera = FindObjectOfType<Camera>();
		}

		public void UpdateByFrame(Playable arg_player) {
			//Playerの座標に合わせて動かす

			Vector3 pos = m_mainCamera.transform.position;
			pos.x = arg_player.transform.position.x;

			//はみ出し防止
			//左
			if (pos.x < 0) { pos.x = 0; }


			m_mainCamera.transform.position = pos;
		}
	}
}