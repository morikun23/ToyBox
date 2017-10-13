using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class Item : ObjectBase {

		public enum State {
			GRABBED,
			CARRIED
		}

		public State m_currentState;
		
		/// <summary>
		/// 掴まれたときの処理
		/// 最初の一度だけ呼ばせること
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnGraspedEnter(PlayerComponent arg_player) {
			
		}

		/// <summary>
		/// 掴まれているときの処理
		/// ここでは掴まれている間常に呼ばれる
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnGraspedStay(PlayerComponent arg_player) {
			//TODO:掴まれているの処理
			//Armを縮めるのか、固定なのか、自分もついていくのか
			//また、持ち運ばれるのか

		}

		/// <summary>
		/// 掴まれていた状態から離された時の処理
		/// ここでは一度だけ呼ばせること
		/// </summary>
		/// <param name="arg_player"></param>
		public virtual void OnGraspedExit(PlayerComponent arg_player) {
			//TODO:掴まれているの処理
			//Armを縮めるのか、固定なのか、自分もついていくのか
			//また、持ち運ばれるのか
		}


	//	public abstract bool IsAbleRelease ();

	//	public abstract bool IsAbleGrasp ();

	}
}