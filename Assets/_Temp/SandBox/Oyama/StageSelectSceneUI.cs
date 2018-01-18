using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class StageSelectSceneUI : MonoBehaviour
    {

        private const string BUTTON_SPRITE_PATH = "Contents/StageSelect/Textures/BD_Player";

        private bool m_isStageSelect;
        private bool m_isHalfPointSelect;
        private bool m_isBack;

        /// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
        {
            
        }

        private void OnBackSelectedPress()
        {
            m_isBack = true;
        }

        private void OnBackSelectedRelease()
        {
            m_isBack = false;

        }

        private void OnStageSelectedPress(object arg_stageId)
        {
            uint id = uint.Parse(arg_stageId.ToString());

            AppManager.Instance.user.m_temp.m_playStageId = id;

            m_isStageSelect = true;

        }

        private void OnStageSelectedRelease()
        {

            m_isStageSelect = false;

        }

        
        /// <summary>
		/// ステージは選んだか？
		/// </summary>
        public bool IsStageSelected()
        {
            return m_isStageSelect;
        }

        /// <summary>
        /// 中間地点は選んだか？
        /// </summary>
        public bool IsHalfPointSelected()
        {
            return m_isHalfPointSelect;
        }

        /// <summary>
        ///　戻るボタンが押されたか？
        /// </summary>
        public bool IsBackButtonSelected()
        {
            return m_isBack;
        }
    }

}