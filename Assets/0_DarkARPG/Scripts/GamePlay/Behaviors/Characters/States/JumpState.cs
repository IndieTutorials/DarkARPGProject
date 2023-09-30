using UnityEngine;
using Animancer;
using RustedGames.Data;

namespace RustedGames
{
    public class JumpState : CharacterState
    {
        [SerializeField]
        private ClipTransition _MainAnimation;
        public ClipTransition MainAnimation => _MainAnimation;

        private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

        public override bool CanEnterState => Character.CharacterMovement.Sprint;
        public override bool CanExitState => true;

        private void Awake()
        {
            Weapon.OnWeaponChanged += UpdateAnimationSet;
            ChangeAnimationSet();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            if (_LocalCurrentAnimationSet != Character.CurrentAnimationSet)
            {
                ChangeAnimationSet();
            }

            Character.CharacterAnimancerComponent.Play(_MainAnimation);
        }

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        private void ChangeAnimationSet()
        {
            _MainAnimation = Character.CurrentAnimationSet.Jump;
            _MainAnimation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
        }


        private void OnDestroy()
        {
            Weapon.OnWeaponChanged -= UpdateAnimationSet;
        }
    }
}
