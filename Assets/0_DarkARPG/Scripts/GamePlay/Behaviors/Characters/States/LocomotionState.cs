using UnityEngine;
using Animancer;
using RustedGames.Data;

namespace RustedGames
{
    public class LocomotionState : CharacterState
    {
        [SerializeField]
        private ClipTransition _Idle;
        public ClipTransition Idle => _Idle;

        [SerializeField]
        private ClipTransition _Walk;
        public ClipTransition Walk => _Walk;

        [SerializeField]
        private ClipTransition _Jog;
        public ClipTransition Jog => _Jog;

        [SerializeField]
        private ClipTransition _Sprint;
        public ClipTransition Sprint => _Sprint;

        private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

        public ClipTransition CurrentAnimation
        {
            get
            {
                if (Character.MovementDirection != Vector2.zero)
                {
                    if (Character.MovementDirection.magnitude < 0.98 && Character.MovementDirection.magnitude > 0)
                    {
                        return _Walk;
                    }
                    else if (Character.MovementDirection.magnitude > 0.99)
                    {
                        if (Character.CharacterMovement.Sprint && Character.HasStamina)
                        {
                            return _Sprint;
                        }
                        else
                        {
                            return _Jog;
                        }                       
                    }
                }

                return _Idle;
            }
        }


        private void Awake()
        {
            Weapon.OnWeaponChanged += UpdateAnimationSet;
            ChangeAnimationSet();
        }

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        public override bool CanEnterState => !Character.CharacterMovement.LockOnTarget;
        public override void OnEnterState()
        {
            base.OnEnterState();

            if (_LocalCurrentAnimationSet != Character.CurrentAnimationSet)
            {
                ChangeAnimationSet();
            }

            Character.CharacterAnimancerComponent.Play(CurrentAnimation);
        }

        protected virtual void Update()
        {
            Character.CharacterAnimancerComponent.Play(CurrentAnimation);
        }

        private void ChangeAnimationSet()
        {
            _Idle = Character.CurrentAnimationSet.Idle;
            _Walk = Character.CurrentAnimationSet.Walk;
            _Jog = Character.CurrentAnimationSet.Jog;
            _Sprint = Character.CurrentAnimationSet.Sprint;
        }

        private void OnDestroy()
        {
            Weapon.OnWeaponChanged -= UpdateAnimationSet;
        }

    }
}
