using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToyBox;
namespace ToyBox
{

    public class OptionModal : ModalController
    {
        //ステージ選択に戻るボタン
        [SerializeField]
        private UIButton m_backStageSelectButton;

        //リトライボタン
        [SerializeField]
        private UIButton m_retryButton;

        //BGMの音量調節バー
        [SerializeField]
        private Scrollbar m_bgmBar;

        //SEの音量調節バー
        [SerializeField]
        private Scrollbar m_seBar;

        [HideInInspector]
        public string m_message;

        [SerializeField]
        private AudioManager m_audio;

        ///<summary>ステージ選択に戻るボタンが押された時のコールバック</summary>
        public System.Action m_playActionBackStageSelect;
        ///<summary>リトライボタンが押された時のコールバック</summary>
        public System.Action m_playRetry;

        ///<summary>BGMバーを押した時のコールバック </summary>
        public System.Action m_playBgmBar;
        ///<summary>SEバーを押した時のコールバック </summary>
        public System.Action m_playSeBar;

        public override void OnActive()
        {
            m_backStageSelectButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress, OnBackSelectButtonPress));
            m_retryButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress, OnRetryButtonPress));

            if (m_audio == null)
            {
                m_audio = FindObjectOfType<AudioManager>();
            }
            //BGM・SEバーの値をAudioManagerの値に合わせる
            m_bgmBar.value = m_audio.BGMVolume;
            m_seBar.value = m_audio.SEVolume;
        }

        public void OnBackSelectButtonPress()
        {
            ///<summary>ステージ選択に戻るボタンが押された時のコールバック</summary>
            m_playActionBackStageSelect();

            UIManager.Instance.PopupYesNoModal("ステージ選択に戻りますか？", () =>
            {
                //Yesボタンを押した際の処理 
                UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
            }, () =>
            {
                //Noボタンを押した際の処理
                UIManager.Instance.PopupOptionModal(() => { });
            });
            Hide();
        }

        public void OnRetryButtonPress()
        {
            m_playRetry();
            UIManager.Instance.PopupYesNoModal("ステージをやり直しますか？", () =>
            {
                //Yesボタンを押した際の処理
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }, () =>
            {
                //Noボタンを押した際の処理
                UIManager.Instance.PopupOptionModal(() => { });
            });
            Hide();
        }

        //BGMバーを動かしたときの処理
        public void OnBgmBarSwipe()
        {
            m_audio.BGMVolume = m_bgmBar.value;
        }

        //SEバーを動かしたときの処理
        public void OnSeBarSwipe()
        {
            m_audio.SEVolume = m_seBar.value;
        }
    }
}

