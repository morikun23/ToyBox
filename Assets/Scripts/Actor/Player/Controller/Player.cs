//担当：森田　勝
//概要：ゲーム内のプレイアブルキャラクターのコントローラークラス
//　　　マジックハンドの制御は自身がトリガーとなる
//　　　ロジックと描画のクラスを制御する
//参考：特になし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Controller {
	public class Player : MonoBehaviour {

		//ロジック部
		public Logic.Player m_logic { get; private set; }

		//描画部
		private View.Player m_view { get; set; }

		//RigidBody
		private Rigidbody2D m_rigidbody { get; set; }
		
		private BoxCollider2D m_collider { get; set; }

		public Arm m_arm { get; private set; }
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

			InputKey();

			if (Input.GetKeyDown(KeyCode.Z) && IsGrounded()) {
				Jump();
			}
			
			m_logic.UpdateByFrame(this);

			m_arm.UpdateByFrame(this);
			m_magicHand.UpdateByFrame(this);

			m_view.UpdateByFrame(m_logic);
		}

		public bool IsGrounded() {
			Vector3 rightBottom = transform.position + Vector3.right * m_collider.bounds.size.x / 2 + Vector3.down * m_collider.bounds.size.y /2;
			Vector3 leftBottom = transform.position + Vector3.left * m_collider.bounds.size.x / 2 + Vector3.down * m_collider.bounds.size.y / 2;

			
			if (Physics2D.Raycast(rightBottom , Vector2.down , 0.2f,1 << LayerMask.NameToLayer("Ground")) ||
				Physics2D.Raycast(leftBottom , Vector2.down , 0.2f, 1 << LayerMask.NameToLayer("Ground"))) {
				return true;
			}
			else{
				return false;
			}
		}

		/// <summary>
		/// Unity側から衝突の情報を取得する
		/// </summary>
		/// <param name="arg_collsionInfo">衝突した相手の情報</param>
		private void OnCollisionEnter2D(Collision2D arg_collsionInfo) {

		}

		/// <summary>
		/// プレイヤーを移動させる
		/// 特に外部からプレイヤーに対して移動をさせたいときに使用する
		/// </summary>
		/// <param name="arg_direction">移動方向</param>
		public void Move(Logic.CharacterBase.Direction arg_direction) {
			switch (arg_direction) {
				case Logic.CharacterBase.Direction.LEFT:
					if(m_logic.m_currentState != typeof(Logic.PlayerRunLeft))
					m_logic.StateTransition(new Logic.PlayerRunLeft());
				break;

				case Logic.CharacterBase.Direction.RIGHT:
					if(m_logic.m_currentState != typeof(Logic.PlayerRunRight))
					m_logic.StateTransition(new Logic.PlayerRunRight());
				break;
			}
		}

		

		/// <summary>
		/// プレイヤーを静止させる
		/// 特に外部からプレイヤーに対して移動をさせたいときに使用する
		/// </summary>
		public void Idle() {
			m_logic.StateTransition(new Logic.PlayerIdle());
		}

		/// <summary>
		/// プレイヤーをジャンプさせる
		/// 特に外部からプレイヤーに対して移動をさせたいときに使用する
		/// </summary>
		public void Jump() {
			m_rigidbody.AddForce(Vector2.up * 200);
		}

		private void InputKey() {
			
			#region PCキーボード入力
			if (Input.GetKey(KeyCode.RightArrow)) {
				Move(Logic.CharacterBase.Direction.RIGHT);
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				Move(Logic.CharacterBase.Direction.LEFT);
			}
			if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
				Idle();
			}
			#endregion
		}
		
		private void InputSwipe() {
			#region スマホスワイプ入力
			if (Input.touchCount > 0) {
				Touch touchInfo = Input.GetTouch(0);
				if (touchInfo.deltaPosition.x > 10) {
					Move(Logic.CharacterBase.Direction.RIGHT);
				}
				if (touchInfo.deltaPosition.x < -10) {
					Move(Logic.CharacterBase.Direction.LEFT);
				}
			}
			else {
				Idle();
			}
			#endregion
		}

		
	}
}