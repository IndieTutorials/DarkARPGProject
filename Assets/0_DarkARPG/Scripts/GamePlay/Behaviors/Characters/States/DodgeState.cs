using UnityEngine;
using Animancer;
using RustedGames.Data;

namespace RustedGames
{
    public sealed class DodgeState : CharacterState
    {
        [SerializeField]
        private ClipTransition _Backstep;
        public ClipTransition Backstep => _Backstep;

        [SerializeField]
        private ClipTransition _Roll;
        public ClipTransition Roll => _Roll;

        [SerializeField]
        private DirectionalAnimationSet8 _DirectionalDash;

        private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

        public override bool CanEnterState => Character.CharacterMovement.Dodge;

        public override bool CanExitState => true;

        public ITransition CurrentAnimation
        {
            get
            {
                if (!Character.CharacterMovement.LockOnTarget)
                {

                    if (Character.MovementDirection != Vector2.zero)
                    {
                        return _Roll;
                    }
                    else
                    {
                        return _Backstep;
                    }
                }
                else
                {
                    return _DirectionalDash.GetClip(Character.MovementDirection);
                }
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            if (_LocalCurrentAnimationSet != Character.CurrentAnimationSet)
            {
                ChangeAnimationSet();
            }

            Character.CharacterAnimancerComponent.Play(CurrentAnimation);
        }

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        private void Awake()
        {
            Weapon.OnWeaponChanged += UpdateAnimationSet;
            ChangeAnimationSet();
        }

        private void OnAnimationEnded()
        {
            Character.CharacterMovement.VariableHandler.SetWithoutNotify
                (GameConstants.DodgeObjectVariableKey, false);

            Character.StateMachine.TrySetDefaultState();
        }

        private void ChangeAnimationSet()
        {
            _Backstep = Character.CurrentAnimationSet.BackStep;
            _Roll = Character.CurrentAnimationSet.Roll;
            _DirectionalDash = Character.CurrentAnimationSet.DirectionalDash;

            _Backstep.Events.OnEnd += OnAnimationEnded;
            _Roll.Events.OnEnd += OnAnimationEnded;

            foreach(ClipTransition clipTransition in _DirectionalDash.GetClips())
            {
                clipTransition.Events.OnEnd += OnAnimationEnded;
            }
        }

        private void OnDestroy()
        {
            Weapon.OnWeaponChanged -= UpdateAnimationSet;
        }

    }
}