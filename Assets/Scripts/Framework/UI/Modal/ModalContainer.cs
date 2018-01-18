using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public partial class ResourcePath {

		/// <summary>モーダル全般の親パス</summary>
		public const string MODAL_PATH = "Contents/UI/Modal/Prefabs/";

		/// <summary>メッセージモーダル</summary>
		public const string MESSAGE_MODAL_PATH = MODAL_PATH + "MessageModal";

	}

	public static class ModalContainer {

		/// <summary>メッセージモーダルのプレハブ</summary>
		public static readonly GameObject MESSAGE_MODAL = Resources.Load<GameObject>(ResourcePath.MESSAGE_MODAL_PATH);
	}
}