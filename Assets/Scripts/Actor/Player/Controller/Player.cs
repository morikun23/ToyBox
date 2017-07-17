//担当：森田　勝
//概要：ゲーム内のプレイアブルキャラクターのコントローラークラス
//　　　マジックハンドの制御は自身がトリガーとなる
//　　　ロジックと描画のクラスを制御する
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Controller {
	public class Player : Playable {

		//ロジック部
		[SerializeField]
		private Logic.Player m_logic;

		//描画部
		private View.Player m_view { get; set; }

		//RigidBody
		private Rigidbody2D m_rigidbody { get; set; }
		//Collider
		private BoxCollider2D m_collider { get; set; }

		//アーム
		public Arm m_arm { get; private set; }
		//マジックハンド
		public MagicHand m_magicHand { get; private set; }

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize() {

			m_rigidbody = GetComponent<Rigidbody2D>();
			m_collider = GetComponent<BoxCollider2D>();

			m_logic = new Logic.Player();
			m_view = GetComponent<View.Player>();
			m_logic.Initialize(this);
			m_view.Initialize(m_logic);

			m_arm = GetComponentInChildren<Arm>();
			m_arm.Initialize(this);

			m_magicHand = GetComponentInChildren<MagicHand>();
			m_magicHand.Initalize(this);
		
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void UpdateByFrame() {

			m_logic.UpdateByFrame(this);

			m_arm.UpdateByFrame(this);
			m_magicHand.UpdateByFrame(this);

			m_view.UpdateByFrame(m_logic);
		}

		/// <summary>
		/// 地面に着いているかを調べる
		/// </summary>
		/// <returns>着地しているか</returns>
		public bool IsGrounded() {
			return Physics2D.BoxCast(transform.position , m_collider.bounds.size , 0f , Vector2.down , 0.05f , 1 << LayerMask.NameToLayer("Ground"));
		}		

		//=============================================
		//　以下、Playableの実装
		//=============================================

		/// <summary>
		/// Action1
		/// 左へ移動させる
		/// めり込み防止の処理も行う
		/// </summary>
		public override void Action1() {
			if (!Physics2D.BoxCast(transform.position , m_collider.bounds.size , 0f , Vector2.left , 0.1f , 1 << LayerMask.NameToLayer("Ground"))){
				m_logic.AddTask(new Logic.PlayerRunLeftCommand(m_logic));
			}
		}

		/// <summary>
		/// Action2
		/// 右へ移動させる
		/// めり込み防止の処理も行う
		/// </summary>
		public override void Action2() {
			if (!Physics2D.BoxCast(transform.position , m_collider.bounds.size , 0f , Vector2.right , 0.1f , 1 << LayerMask.NameToLayer("Ground"))) {
				m_logic.AddTask(new Logic.PlayerRunRightCommand(m_logic));
			}
		}

		/// <summary>
		/// Action3
		/// ジャンプさせる
		/// </summary>
		public override void Action3() {
			if (IsGrounded()) {
				m_logic.AddTask(new Logic.PlayerJumpCommand(m_logic , m_rigidbody));
			}
		}

		/// <summary>
		/// Action4
		/// マジックハンドを射出し始める
		/// </summary>
		public override void Action4() {
			m_logic.AddTask(new Logic.PlayerReachCommand(m_logic,m_rigidbody,m_arm.m_logic));
		}
	}
}