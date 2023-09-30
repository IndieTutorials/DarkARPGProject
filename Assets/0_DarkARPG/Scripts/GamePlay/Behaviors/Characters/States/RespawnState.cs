using UnityEngine;
using Animancer;

namespace RustedGames
{
    public sealed class RespawnState : CharacterState
    {
        [SerializeField] private ClipTransition _MainAnimation;

        private void Awake()
        {
           _MainAnimation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
           //Character.StateMachine.TrySetState(this);
        }

        public override void OnEnterState()
        {
            Character.CharacterAnimancerComponent.Play(_MainAnimation);
        }

        public override bool CanExitState => false;

        public override void OnExitState()
        {
           
        }
    }
}
