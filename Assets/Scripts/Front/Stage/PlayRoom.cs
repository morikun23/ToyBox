using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	
	public class PlayRoom : MonoBehaviour {

		#region DictionaryをSerialize化させるための記述
		[System.Serializable]
		protected class BorderTable : Serialize.DictionaryBase<RoomCollider , uint , PlayRoomBorder> { }

		[System.Serializable]
		protected class PlayRoomBorder : Serialize.KeyAndValue<RoomCollider , uint> {
			public PlayRoomBorder(RoomCollider key , uint value) : base(key , value) { }
		}
		#endregion

		///<summary>部屋番号</summary>
		[SerializeField,Header("部屋番号")]
		private uint m_id;

		/// <summary>一度でもこの部屋に入ったか</summary>
		[SerializeField]
		private bool m_isEntered;

		/// <summary>クリア済みか</summary>
		[SerializeField]
		private bool m_isSuccessed;

		/// <summary>初期座標</summary>
		[SerializeField]
		private Transform m_startPoint;

		/// <summary>エントリーポイントのリスト</summary>
		[SerializeField,Header("入り口となるコライダー:Collider/隣の部屋番号")]
		private BorderTable m_enterBordars;

		/// <summary>この部屋に入ったときに実行するコールバック</summary>
		private System.Action<PlayRoom> m_onEnterRoomAction;

		/// <summary>この部屋に含まれているギミックたち</summary>
		private readonly List<GameObject> m_gimmicks = new List<GameObject>();

		/// <summary>
		/// 部屋番号
		/// ※読み取り専用
		/// </summary>
		public uint Id {
			get {
				return m_id;
			}
		}

		/// <summary>
		/// この部屋に一度でも入ったか
		/// ※読み取り専用
		/// </summary>
		public bool IsEntered {
			get {
				return m_isEntered;
			}
		}

		/// <summary>
		/// クリア済みか
		/// ※読み取り専用
		/// </summary>
		public bool IsSuccessed {
			get {
				return m_isSuccessed;
			}
		}

		/// <summary>
		/// リスタート地点
		/// ※読み取り専用
		/// </summary>
		public Transform RestartPoint {
			get {
				return m_startPoint;
			}
		}

		/// <summary>
		/// この部屋に含まれているギミックたち
		/// </summary>
		public List<GameObject> Gimmicks {
			get {
				//一応コピーしたものを渡す
				return new List<GameObject>(m_gimmicks);
			}
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="arg_onEnterAction">コールバック</param>
		public void Initialize(System.Action<PlayRoom> arg_onEnterRoomAction) {

			if(arg_onEnterRoomAction == null) {
				Debug.LogError("コールバックが設定されていません");
			}

			this.m_onEnterRoomAction = arg_onEnterRoomAction;

			foreach (KeyValuePair<RoomCollider , uint> playRoomBorder in m_enterBordars.Dictionary) {
				playRoomBorder.Key.m_roomId = this.Id;
				playRoomBorder.Key.m_prevRoomId = playRoomBorder.Value;
				playRoomBorder.Key.Initialize(this.OnRoomEnter);
			}
			
			//GameObjectはGetComponentで取得できないので変換する必要がある
			foreach(Transform gimmick in transform.Find("Gimmicks").GetComponentsInChildren<Transform>()) {
				m_gimmicks.Add(gimmick.gameObject);
			}
			
		}

		/// <summary>
		/// この部屋に入ったときにコライダーから処理されるためのコールバック
		/// </summary>
		/// <param name="arg_prevId">どの部屋から入ってきたか</param>
		private void OnRoomEnter(uint arg_prevId) {
			m_isEntered = true;
			this.m_onEnterRoomAction(this);
		}

		/// <summary>
		/// 隣り合っている部屋番号を全て取得する
		/// </summary>
		/// <returns>部屋番号のリスト</returns>
		public List<uint> GetSideRoomsId() {
			List<uint> sideRoomId = new List<uint>();
			foreach(uint roomId in m_enterBordars.Dictionary.Values) {
				if (!sideRoomId.Contains(roomId)) {
					sideRoomId.Add(roomId);
				}
			}
			return sideRoomId;
		}

		/// <summary>
		/// ゲームをクリア状態にする
		/// </summary>
		public void DoneRoomSuccessed() {
			m_isSuccessed = true;
		}

		/// <summary>
		/// ギミックを未クリア状態に戻す
		/// </summary>
		public void ResetGimmick() {
			m_isSuccessed = false;
		}

	}

}