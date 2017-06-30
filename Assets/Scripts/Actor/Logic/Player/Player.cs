using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Logic {
	[System.Serializable]
	public class Player : ActorBuddy {
		
		public IPlayerState m_currentState { get; private set; }
		public Stack<IPlayerCommand> m_currentCommands { get; private set; }

		public Arm m_arm { get; private set; }
		public MagicHand m_magicHand { get; private set; }
		
		public Player() : base(new Vector2(0,0),0,128,128,1.0f){
			m_arm = new Arm();
			m_magicHand = new MagicHand();
			m_currentCommands = new Stack<IPlayerCommand>();
			this.Initialize();
		}

		public override void Initialize() {
			m_currentState = new PlayerIdle();
			m_arm.Initialize(this);
			m_magicHand.Initialize(this);
		}

		public override void UpdateByFrame(){
			if (m_currentState != null) {
				m_currentState.OnUpdate(this);
			}

			if (m_currentCommands.Count > 0) {
				m_currentCommands.Peek().OnUpdate(this);
			}
			
			m_arm.UpdateByFrame(this);
			m_magicHand.UpdateByFrame(this);
		}
		
		public void StateTransition(IPlayerState arg_nextState) {
			m_currentState.OnExit(this);
			m_currentState = arg_nextState;
			m_currentState.OnEnter(this);
		}

		public void AddCommand(IPlayerCommand arg_Command) {
			m_currentCommands.Push(arg_Command);
			m_currentCommands.Peek().OnEnter(this);
		}

		public void Move(Direction arg_direction) {
			StateTransition(new PlayerRun());
			m_direction = arg_direction;
		}
		
	}
}