using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	public abstract class ActorBuddy : ActorBase {

		//移動速度
		public float m_speed;

		//向き
		public enum Direction {
			RIGHT = 1,
			LEFT = -1
		}

		//現在の向き
		public Direction m_direction;

		protected ActorBuddy(
			Vector2 arg_position ,
			float arg_rotation ,
			float arg_width ,
			float arg_height ,
			float arg_speed ,
			Direction arg_direction = Direction.RIGHT,
			bool arg_isActive = true
			) : base(arg_position,arg_rotation,arg_width,arg_height,arg_isActive) {
			m_speed = arg_speed;
			m_direction = arg_direction;
		}

		public abstract void Initialize();

		public abstract void UpdateByFrame();
	}
}