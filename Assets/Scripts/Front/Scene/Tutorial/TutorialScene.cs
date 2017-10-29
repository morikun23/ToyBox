using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox {
	public class TutorialScene : ToyBox.Scene {

		private Stage m_stage;

		private Player m_player;

		private InputManager m_inputManager;

		[SerializeField]
		private Animator m_doorAnimation;

		//最初に読ませる看板
		private IntrodcutionSignBoard m_introduction;

		public override IEnumerator OnEnter() {

			//ステージの生成（どのステージなのかを区別させたい）
			m_stage = FindObjectOfType<Stage>();

			//プレイヤーの生成
			m_player = FindObjectOfType<Player>();

			m_stage.Initialize(m_player);

			//入力環境を初期化
			m_inputManager = GetComponent<InputManager>();
			m_inputManager.Initialize();

			m_introduction = FindObjectOfType<IntrodcutionSignBoard>();

			m_introduction.m_animation.SetActive(false);

			AppManager.Instance.m_fade.StartFade(new FadeIn() , Color.black , 1.0f);
			yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

			

			yield return new WaitForSeconds(0.5f);
		}

		public override IEnumerator OnUpdate() {
			#region 最初の看板を読ませるための演出
			m_inputManager.InputStop();
			
			m_introduction.m_animation.SetActive(true);
			while (true) {
				m_inputManager.UpdateByFrame();

				if (m_introduction.IsRead()) {
					m_introduction.m_animation.SetActive(false);
					break;
				}
				yield return null;
			}
			
			m_inputManager.InputStart();

			#endregion

			while (true) {
				if (m_player.GetCurrentState() != typeof(PlayerDeadState)) {
					m_inputManager.UpdateByFrame();
				}
				if (m_stage.DoesPlayerReachGoal()) {
					m_doorAnimation.SetBool("Open" , true);

					yield return null;
					yield return new Tsubakit.WaitForAnimation(m_doorAnimation , 0);
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

			SceneManager.LoadScene("Select");
			yield return null;

		}
	}
}