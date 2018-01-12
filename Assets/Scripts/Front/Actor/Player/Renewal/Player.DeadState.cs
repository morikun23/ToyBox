using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Develop {
	public partial class Player {
		private class DeadState : IPlayerState {

			/// <summary>
			/// プレイヤー自身への参照
			/// </summary>
			private Player _;

			public DeadState(Player arg_player) {
				_ = arg_player;
			}

			void IPlayerState.OnEnter() {

			}

			void IPlayerState.OnExit() {

			}

			void IPlayerState.OnUpdate() {

			}

			IPlayerState IPlayerState.GetNextState() {
				return null;
			}

		}
	}
}