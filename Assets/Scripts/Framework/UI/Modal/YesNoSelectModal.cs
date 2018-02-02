using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;
using UnityEngine.UI;

namespace ToyBox{
	public class YesNoSelectModal : ModalController {

		//Yesボタン
		[SerializeField]
		private UIButton m_yesButton;

		//Noボタン
		[SerializeField]
		private UIButton m_noButton;

		//表示するテキスト
		[SerializeField]
		private Text m_text;
		[HideInInspector]
		public string m_message;

		/// <summary>Yesボタンが押されたときのコールバック</summary>
		public System.Action m_playActionYes;
		/// <summary>Noボタンが押されたときのコールバック</summary>
		public System.Action m_playActionNo;

		public override void OnActive (){
			m_yesButton.Initialize(new ButtonAction(ButtonEventTrigger.OnPress,OnYesButtonPress));
			m_noButton.Initialize (new ButtonAction(ButtonEventTrigger.OnPress,OnNoButtonPress));
			m_text.text = m_message;
		}
			
		public void OnYesButtonPress(){
			m_playActionYes ();
			Hide ();
		}

		public void OnNoButtonPress(){
			m_playActionNo ();
			Hide ();
		}

	}	
}
