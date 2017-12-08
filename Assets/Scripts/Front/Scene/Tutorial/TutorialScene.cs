using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyBox
{
    public class TutorialScene : ToyBox.Scene
    {

        private Stage m_stage;

        private Player m_player;

        private InputManager m_inputManager;

        [SerializeField]
        private Animator m_doorAnimation;

        public override IEnumerator OnEnter()
        {
            
            //ステージの生成（どのステージなのかを区別させたい）
            m_stage = FindObjectOfType<Stage>();

            //プレイヤーの生成
            m_player = FindObjectOfType<Player>();

            m_stage.Initialize(m_player);

            //入力環境を初期化
            m_inputManager = GetComponent<InputManager>();
           //m_inputManager.Initialize();

            AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

            yield return new WaitForSeconds(1.5f);
        }

        public override IEnumerator OnUpdate()
        {

            while (true)
            {
//                if (m_player.GetCurrentState() != typeof(PlayerDeadState))
//                {
//                    //m_inputManager.UpdateByFrame();
//                }
                if (m_stage.DoesPlayerReachGoal())
                {
                    m_doorAnimation.SetBool("Open", true);

                    yield return null;
                    yield return new Tsubakit.WaitForAnimation(m_doorAnimation, 0);
                    break;
                }
                yield return null;
            }

            #region ゴール後の演出
            AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.white, 3.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
            #endregion
        }

        public override IEnumerator OnExit()
        {

            SceneManager.LoadScene("Select");
            yield return null;

        }
    }

}