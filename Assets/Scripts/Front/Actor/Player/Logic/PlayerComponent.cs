﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class PlayerComponent : Playable {

		//--------------------------
		//UnityComponent
		//--------------------------

		//RigidBody
		public Rigidbody2D m_rigidbody { get; protected set; }

		//Collider
		public BoxCollider2D m_collider { get; protected set; }

		//--------------------------
		//パラメータ
		//--------------------------

		//自身の状態（Stateパターンでの実装）
		public IPlayerState m_currentState { get; protected set; }

		//地面に接しているか
		public bool m_isGrounded { get; protected set; }

		protected PlayerViewer m_viewer { get; set; }

		public abstract ArmComponent Arm { get; }

		public abstract HandComponent Hand { get; }
		
		/// <summary>
		/// 地面に着いているかを調べる
		/// </summary>
		/// <returns>着地しているか</returns>
		public bool IsGrounded() {
			return Physics2D.BoxCast(transform.position , m_collider.bounds.size , 0f , Vector2.down , 0.1f , 1 << LayerMask.NameToLayer("Ground"));
		}

		/// <summary>
		/// ステートを遷移させる
		/// </summary>
		/// <param name="arg_nextState">次の状態</param>
		protected void StateTransition(IPlayerState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}
	}
}