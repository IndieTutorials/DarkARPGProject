using Animancer.FSM;
using UnityEngine;

namespace RustedGames
{
    [DefaultExecutionOrder(DefaultExecutionOrder)]
    public abstract class CharacterState : StateBehaviour, IOwnedState<CharacterState>
    {

        public const int DefaultExecutionOrder = -1000;

        [SerializeField]
        private Character _Character;
        public Character Character => _Character;
        public StateMachine<CharacterState> OwnerStateMachine => _Character.StateMachine;
    }
}
