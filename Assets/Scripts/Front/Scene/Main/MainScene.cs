//担当：森田　勝
//概要：メインシーン全体を管理するクラス
//　　　シーン内の処理はこのクラスをトリガーとしてください。
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox {
	public class MainScene : ToyBox.Scene {

		private Stage m_stage;

		private Player m_player;

		private InputManager m_inputManager;

		#region 入場時の演出に使用する
		[SerializeField]
		private Animator m_doorAnimation;

		private Color m_colorBuf;
		#endregion

		public override IEnumerator OnEnter() {
			//ステージの生成（どのステージなのかを区別させたい）
			m_stage = FindObjectOfType<Stage>();

			//プレイヤーの生成
			m_player = FindObjectOfType<Player>();
			m_colorBuf = m_player.m_viewer.m_spriteRenderer.color;
			m_player.m_viewer.m_spriteRenderer.color = new Color(m_colorBuf.r,m_colorBuf.g,m_colorBuf.b,0);

			m_stage.Initialize(m_player);

			//入力環境を初期化
			m_inputManager = GetComponent<InputManager>();
			//m_inputManager.Initialize();

			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

			yield return new WaitForSeconds(1.5f);

			#region 入場時の演出
			m_doorAnimation.SetBool("Open",true);
			yield return null;
			yield return new Tsubakit.WaitForAnimation(m_doorAnimation , 0);

			float alpha = 0;
			
			while (alpha < 1) {
				alpha += 0.01f;
				m_player.m_viewer.m_spriteRenderer.color = new Color(m_colorBuf.r , m_colorBuf.g , m_colorBuf.b , alpha);
				yield return null;
			}

			#endregion
		}

		public override IEnumerator OnUpdate() {

			while (true) {
//				if(m_player.GetCurrentState() != typeof(PlayerDeadState)){
//					m_inputManager.UpdateByFrame();
//				}
				if (m_stage.DoesPlayerReachGoal()) {
					break;
				}
				yield return null;
			}

			#region ゴール後の演出
			AppManager.Instance.m_fade.StartFade(new FadeOut() , Color.white , 3.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
			#endregion
		}

		public override IEnumerator OnExit() {
			
			SceneManager.LoadScene("Result");
			yield return null;

		}
	}
}