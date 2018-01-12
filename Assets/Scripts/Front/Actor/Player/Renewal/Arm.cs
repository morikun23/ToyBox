using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {

	public interface IArmCallBackReceiver {

		/// <summary>
		/// アームを伸ばし始めたときに実行される
		/// </summary>
		void OnStartLengthen();

		/// <summary>
		/// アームが縮み終わったときに実行される
		/// </summary>
		void OnEndShorten();
	}

	public class Arm : MonoBehaviour , IHandCallBackReceiver {

		[Header("部位")]

		/// <summary>アームの先端となるGameObject</summary>
		[SerializeField]
		private GameObject m_top;

		/// <summary>アームの根本となるGameObject</summary>
		[SerializeField]
		private GameObject m_bottom;
		
		[Header("性能")]

		/// <summary>伸縮速度</summary>
		[SerializeField]
		private float m_reachSpeed = 0.5f;

		/// <summary>射程距離</summary>
		[SerializeField]
		private float m_range = 10;

		[Header("描画用")]

		/// <summary>直線描画用コンポーネント</summary>
		[SerializeField]
		private LineRenderer m_lineRenderer;

		/// <summary>描画時の重なり度</summary>
		[SerializeField]
		private float m_depth;

		/// <summary>この座標まで腕を伸ばす</summary>
		private Vector2 m_targetPosition;

		/// <summary>腕の根元から目標までの単位ベクトル</summary>
		private Vector2 m_targetDirection;

		/// <summary>現在の腕の長さ</summary>
		private float m_currentLength;
		
		/// <summary>実行する処理</summary>
		private System.Action m_currentTask;

		/// <summary>伸縮毎の長さのバッファ</summary>
		private readonly Stack<float> m_lengthBuf = new Stack<float>();

		private IArmCallBackReceiver m_callBackReceiver;

		/// <summary>
		/// 射程距離
		/// </summary>
		public float Range {
			get {
				return m_range;
			}
		}

		/// <summary>
		/// 現在の長さ
		/// </summary>
		public float CurrentLength {
			get {
				return m_currentLength;
			}
		}
		
		/// <summary>
		/// 先端の座標
		/// </summary>
		public Vector2 TopPosition {
			get {
				return m_top.transform.position;
			}
			private set {
				m_top.transform.position = value;
			}
		}

		/// <summary>
		/// 根元の座標
		/// </summary>
		public Vector2 BottomPosition {
			get {
				return m_bottom.transform.position;
			}
			private set {
				
				transform.parent.position = (Vector3)value - transform.localPosition;
				m_bottom.transform.position = transform.parent.position + new Vector3(0 , 0.35f , 0);
			}
		}
		
		/// <summary>
		/// 初期化/起動メソッド
		/// </summary>
		/// <param name="arg_callBackReceiver">コールバックを受け取る対象</param>
		public void Initialize(IArmCallBackReceiver arg_callBackReceiver) {
			m_callBackReceiver = arg_callBackReceiver;
			StartCoroutine(UpdateByCoroutine());
		}

		/// <summary>
		/// Update処理
		/// </summary>
		private IEnumerator UpdateByCoroutine() {
			while (true) {
				if (m_currentTask != null) {
					m_currentTask();
				}

				m_lineRenderer.SetPosition(0 , new Vector3(BottomPosition.x , BottomPosition.y , -m_depth));
				m_lineRenderer.SetPosition(1 , new Vector3(TopPosition.x , TopPosition.y , -m_depth));

				yield return null;
			}
		}

		/// <summary>
		/// 指定された方向へ射程距離分、腕を伸ばす
		/// </summary>
		/// <param name="arg_direction">腕を伸ばす方向</param>
		public void ReachOut(Vector2 arg_direction) {
			m_targetDirection = arg_direction.normalized;
			m_targetPosition = BottomPosition + m_targetDirection * Range;
			m_currentTask = this.Lengthen;
			m_callBackReceiver.OnStartLengthen();
		}

		/// <summary>
		/// 指定された座標へ腕を伸ばす
		/// この場合、射程距離を無視して指定座標まで手を伸ばし続けます
		/// </summary>
		/// <param name="arg_targetPosition">指定された座標まで手を伸ばす</param>
		public void ReachOutFor(Vector2 arg_targetPosition) {
			m_targetPosition = arg_targetPosition;
			m_targetDirection = (arg_targetPosition - BottomPosition).normalized;
			m_currentTask = this.Lengthen;
			m_callBackReceiver.OnStartLengthen();
		}

		/// <summary>
		/// 腕を伸ばす
		/// </summary>
		private void Lengthen() {

			m_lengthBuf.Push(m_currentLength);

			TopPosition += m_targetDirection * m_reachSpeed;
			m_currentLength = (m_top.transform.position - m_bottom.transform.position).magnitude;

			float targetLength = (m_targetPosition - BottomPosition).magnitude;

			if (m_currentLength > targetLength) {
				m_top.transform.position = m_targetPosition;
				m_currentLength = targetLength;
				m_currentTask = null;
				StartCoroutine(OnLengthenFinished());
			}
		}
		
		/// <summary>
		/// 腕を伸ばし終わったあとの処理
		/// </summary>
		/// <returns></returns>
		private IEnumerator OnLengthenFinished() {

			//当たり判定のコールバックを受け取るために一度待機させる
			yield return new WaitForEndOfFrame();
			
			//コールバックを受け取らなかったら強制的に腕を縮める
			if(m_currentTask == null) {
				m_currentTask = this.ShortenTop;
			}
		}

		/// <summary>
		/// 腕の先端を縮める
		/// </summary>
		private void ShortenTop() {
			if (m_lengthBuf.Count <= 0) {
				m_currentTask = null;
				m_callBackReceiver.OnEndShorten();
				return;
			}
			
			m_currentLength = m_lengthBuf.Pop();
			TopPosition = BottomPosition + m_targetDirection * m_currentLength;

		}

		/// <summary>
		/// 腕の根元を縮める
		/// </summary>
		private void ShortenBottom() {
			if (m_lengthBuf.Count <= 0) {
				m_currentTask = null;
				m_callBackReceiver.OnEndShorten();
				return;
			}

			m_currentLength = m_lengthBuf.Pop();
			BottomPosition = m_targetPosition - m_targetDirection * m_currentLength;
			TopPosition = m_targetPosition;
		}

		void IHandCallBackReceiver.OnCollided() {

		}

		void IHandCallBackReceiver.OnGrasped() {
		
		}

		void IHandCallBackReceiver.OnReleased() {
			
		}
	}
}