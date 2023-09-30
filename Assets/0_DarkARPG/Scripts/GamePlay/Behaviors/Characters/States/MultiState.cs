using UnityEngine;
using Animancer;

namespace RustedGames
{
    [DefaultExecutionOrder(DefaultExecutionOrder)]
    public class MultiState : CharacterState
    {
        [SerializeField, Tooltip("The other states that this one will try to enter in order.")]
        private CharacterState[] _States;
        public CharacterState[] States => _States;
        private CharacterState _CurrentState;

        public override bool CanEnterState => Character.StateMachine.CanSetState(_States);
        public override bool CanExitState => true;

        [SerializeField]
        private bool _AutoInternalTransitions;
        public bool AutoInernalTransitions => _AutoInternalTransitions;

        public override void OnEnterState()
        {
            if (Character.StateMachine.TrySetState(_States))
            {
                if (_AutoInternalTransitions)
                {
                    _CurrentState = Character.StateMachine.CurrentState;
                    enabled = true;
                }
            }
        }

        public override void OnExitState()
        {
            
        }

        protected virtual void Update()
        {
            if (_CurrentState != Character.StateMachine.CurrentState)
            {
                enabled = false;
                return;
            }

            CharacterState newState = Character.StateMachine.CanSetState(_States);
            if (_CurrentState != newState && newState != null)
            {
                _CurrentState = newState;
                Character.StateMachine.ForceSetState(newState);
            }
        }
    }
}
