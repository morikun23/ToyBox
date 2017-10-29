﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class PlayerComponent : Playable {

		//--------------------------
		//UnityComponent
		//--------------------------

		//RigidBody
		public abstract Rigidbody2D m_rigidbody { get; }

		//Collider
		public abstract BoxCollider2D m_body { get; }

		//Collider
		public abstract BoxCollider2D m_foot { get; }


		//--------------------------
		//パラメータ
		//--------------------------

		//自身の状態（Stateパターンでの実装）
		protected IPlayerState m_currentState;
		//自身の地形状態
		protected IPlayerGroundInfo m_currentGroundInfo;
		//自身のギミック効果状態
		protected IPlayerGimmickInfo m_currentGimmickInfo;

		public PlayerViewer m_viewer { get; protected set; }

		public abstract ArmComponent Arm { get; }

		public abstract HandComponent Hand { get; }

		public bool m_ableJump;

		/// <summary>
		/// 地面に着いているかを調べる
		/// </summary>
		/// <returns>着地しているか</returns>
		public bool IsGrounded() {
			return Physics2D.BoxCast(m_foot.bounds.center , m_foot.bounds.size ,
				0f , Vector2.down , m_foot.bounds.size.y / 2 ,
				1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Landable"));
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

		/// <summary>
		/// 地形情報を遷移させる
		/// </summary>
		/// <param name="arg_nextInfo">次の状態</param>
		protected void GroundInfoTransition(IPlayerGroundInfo arg_nextInfo) {
			m_currentGroundInfo.OnExit(this);
			m_currentGroundInfo = arg_nextInfo;
			m_currentGroundInfo.OnEnter(this);
		}

		/// <summary>
		/// ギミック情報を遷移させる
		/// </summary>
		/// <param name="arg_nextInfo">次の状態</param>
		protected void GimmickInfoTransition(IPlayerGimmickInfo arg_nextInfo) {
			m_currentGimmickInfo.OnExit(this);
			m_currentGimmickInfo = arg_nextInfo;
			m_currentGimmickInfo.OnEnter(this);
		}

		/// <summary>
		/// 現在の状態を取得する
		/// </summary>
		/// <returns></returns>
		public System.Type GetCurrentState() {
			return m_currentState.GetType();
		}

		/// <summary>
		/// 現在の地形情報を取得する
		/// </summary>
		/// <returns></returns>
		public System.Type GetGroundInfo() {
			return m_currentGroundInfo.GetType();
		}

		/// <summary>
		/// 現在のギミック情報を取得する
		/// </summary>
		/// <returns></returns>
		public System.Type GetGimmickInfo() {
			return m_currentGimmickInfo.GetType();
		}

		public void Dead() {
			if(GetCurrentState() == typeof(PlayerDeadState)) { return; }
			StateTransition(new PlayerDeadState());
		}

		public void Revive() {
			if(GetCurrentState() != typeof(PlayerDeadState)) { return; }
			StateTransition(new PlayerIdleState());
			m_viewer.transform.position = this.transform.position;
		}
	}
}