using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {
	public class PlayerController : MonoBehaviour {

		[SerializeField]
		Player player;


		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

			if (Input.GetKey(KeyCode.RightArrow)) {
				player.Run(Player.Direction.RIGHT);
			}
			else {
				player.Stop(Player.Direction.RIGHT);
			}


			if (Input.GetKey(KeyCode.LeftArrow)) {
				player.Run(Player.Direction.LEFT);
			}
			else {
				player.Stop(Player.Direction.LEFT);
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				player.Jump();
			}

			if (Input.GetMouseButtonDown(0)) {

				if (player.PlayableHand.IsGrasping) {
					if (player.PlayableHand.GraspingItem.IsAbleRelease()) {
						player.PlayableHand.Release();
					}
				}
				else {
					//player.PlayableArm.ReachOut(Vector2.up);
					player.PlayableArm.ReachOutFor(Camera.main.ScreenToWorldPoint(Input.mousePosition));
				}
			}
			
		}
	}
}
