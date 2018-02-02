using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace ToyBox{
	public class NetworkErrorModal : ModalController{

		/// <summary>再接続ボタン</summary>
		[SerializeField]
		private UIButton m_reloadButton;

		/// <summary>再接続ボタンが押されたときのコールバック</summary>
		public System.Action m_playAction;

		public override void OnActive (){
			m_reloadButton.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease,this.Hide));
		}

		/// <summary>
		/// 再接続ボタンと×ボタンが押された時の処理
		/// </summary>
		private void OnReloadButton(){
			m_playAction();
			this.Hide ();
		}
	}
}