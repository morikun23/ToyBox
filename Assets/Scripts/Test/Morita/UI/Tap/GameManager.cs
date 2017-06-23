using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Tap {
	public class GameManager : MonoBehaviour {

		[SerializeField]
		Lever lever;

		[SerializeField]
		InputArea leftArea;

		[SerializeField]
		InputArea rightArea;

		// Use this for initialization
		void Start() {

			lever.Initialize();

			leftArea.Initialize();
			rightArea.Initialize();

		}

		// Update is called once per frame
		void Update() {

			leftArea.UpdateByFrame(lever);
			rightArea.UpdateByFrame(lever);

			if(!leftArea.isActive && !rightArea.isActive) {
				lever.SetDirection(Lever.Direction.NUTRAL);
			}

			lever.UpdateByFrame();

		}
	}
}