//担当：森田　勝
//概要：アームとしての振る舞いを実行するクラス
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public class Arm : ArmComponent {

		//自身のコンポーネント取得
		[SerializeField]
		GameObject obj_armCollider;
		BoxCollider2D col_armCollider;

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_player"></param>
		public void Initialize() {
			
			m_currentState = new ArmStandByState();
			m_lengthBuf = new Stack<float>();
			m_currentLength = 0f;
			
			m_viewer = GetComponentInChildren<ArmViewer>();
			m_viewer.Initialize(this);

			col_armCollider = obj_armCollider.GetComponent<BoxCollider2D> ();
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="arg_player"></param>
		public void UpdateByFrame() {

			//プレイヤーの座標に依存する
			m_transform.position = m_owner.transform.position;

			IArmState nextState = m_currentState.GetNextState(this);
			if(nextState != null) {
				StateTransition(nextState);
			}
			m_currentState.OnUpdate(this);

			m_viewer.UpdateByFrame(this);


			col_armCollider.size = new Vector2(
				Mathf.Sqrt(Mathf.Pow((GetTopPosition ().x - GetBottomPosition ().x),2) + Mathf.Pow((GetTopPosition ().y - GetBottomPosition ().y),2)),0.1f);

			col_armCollider.transform.eulerAngles = new Vector3 (0,0,
				Mathf.Atan2(GetTopPosition().y - GetBottomPosition().y,GetTopPosition().x - GetBottomPosition().x) * Mathf.Rad2Deg);
			obj_armCollider.transform.localPosition = new Vector3 (
				(GetTopPosition().x - GetBottomPosition().x) / 2,
				(GetTopPosition().y - GetBottomPosition().y) / 2,0);
		}
		
	}
}