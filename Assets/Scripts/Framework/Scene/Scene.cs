using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
	public abstract class Scene : MonoBehaviour {

		//シーン遷移に生成するプレハブ
		protected GameObject m_scenePrefab;

		//シーン遷移時に生成されたGameObject
		protected GameObject m_sceneObject;

		protected Scene(string arg_scenePrefabPass) {
			m_scenePrefab = Resources.Load<GameObject>(arg_scenePrefabPass);
		}
		~Scene() {}

		public virtual void OnEnter() {
			m_sceneObject = Instantiate(m_scenePrefab);
		}

		public virtual void OnUpdate() {
			
		}

		public virtual void OnExit() {
			Destroy(m_sceneObject);
		}
	}
}