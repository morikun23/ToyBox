using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI.Gear {
	public class GameManager : MonoBehaviour {

		Gear gear;
		InputArea inputArea;


		// Use this for initialization
		void Start() {
			gear = FindObjectOfType<Gear>();
			gear.Initialize();
			inputArea = FindObjectOfType<InputArea>();
			inputArea.Initialize(gear);
			
		}

		// Update is called once per frame
		void Update() {

			inputArea.UpdateByFrame(gear);
			gear.UpdateByFrame();
		}
	}
}