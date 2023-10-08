using UnityEngine;
using RustedGames.Data;

namespace RustedGames
{
	public sealed class AttackState : CharacterState
	{
		[SerializeField]
		private AttackTransition _CurrentAnimation;

		private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

		public override bool CanEnterState => Character.CharacterMovement.IsGrounded;

        public override bool CanExitState => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.Events.NormalizedEndTime;

        private void Awake()
        {
            Weapon.OnWeaponChanged += UpdateAnimationSet;
        }

        private void OnEnable()
        {
            if (_LocalCurrentAnimationSet != Character.CurrentAnimationSet)
            {
                ChangeAnimationSet();
            }

            Character.CharacterAnimancerComponent.Play(_CurrentAnimation);
        }

        private void OnDestroy()
        {
            Weapon.OnWeaponChanged -= UpdateAnimationSet;
        }

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        private void ChangeAnimationSet()
        {
            _CurrentAnimation.CopyFrom(Character.CurrentAnimationSet.QuickAttack);
            _CurrentAnimation.Events.OnEnd = OnAnimationEnded;
        }

        private void OnAnimationEnded()
        {
            Character.StateMachine.TrySetDefaultState();
        }
    }
}