using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Morita.UI {
	public class Lever : MonoBehaviour {

		public enum Direction {
			LEFT,
			NUTRAL,
			RIGHT
		}

		public Direction _currentDirection;

		public void Initialize() {
			_currentDirection = Direction.NUTRAL;
		}

		public void UpdateByFrame() {
			switch (_currentDirection) {
				case Direction.NUTRAL:
				transform.eulerAngles = Vector3.zero;
				break;
				case Direction.LEFT:
				transform.eulerAngles = new Vector3(0 , 0 , 40);
				FindObjectOfType<Player>().transform.position += Vector3.left * 0.1f;
				break;
				case Direction.RIGHT:
				transform.eulerAngles = new Vector3(0 , 0 , -40);
				FindObjectOfType<Player>().transform.position += Vector3.right * 0.1f;
				break;
			}

		}

		public void SetDirection(Direction _direc) {
			_currentDirection = _direc;
		}
	}
}