using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox {

	#region DictionaryをSerialize化させるための記述
	[System.Serializable]
	public class StageTable : Serialize.DictionaryBase<uint , GameObject , StageID> { }

	/// <summary>
	/// ジェネリックを隠すために継承してしまう
	/// [System.Serializable]を書くのを忘れない
	/// </summary>
	[System.Serializable]
	public class StageID : Serialize.KeyAndValue<uint , GameObject> {
		public StageID(uint key , GameObject value) : base(key , value) { }
	}
	#endregion

	public class StageFactory : MonoBehaviour {

		[Header("ステージ表：ID/Prefab")]
		[SerializeField]
		StageTable m_stageTable;
		
		/// <summary>
		/// ステージを生成する
		/// </summary>
		/// <param name="arg_id">ステージのID</param>
		/// <returns</returns>
		public Stage Load(uint arg_id) {
			GameObject stage = Instantiate(m_stageTable.Dictionary[arg_id],Vector3.zero,Quaternion.identity);
			return stage.GetComponent<Stage>();
		}

		/// <summary>
		/// ステージを破棄する
		/// </summary>
		public void Remove(Stage arg_stage) {
			DestroyImmediate(arg_stage.gameObject);
		}
	}
}