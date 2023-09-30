using UnityEngine;
using Animancer;
using RustedGames.Data;

namespace RustedGames
{
    public sealed class StrafeLocomotionState : CharacterState
    {

        [SerializeField]
        private DirectionalAnimationSet8 _DirectionalStrafeWalkMovement;

        [SerializeField]
        private DirectionalAnimationSet8 _DirectionalStrafeJogMovement;

        [SerializeField, Range(0,1)]
        private float _MovementLevel;

        [SerializeField]
        private float _MagnitudeThreshold = 0.98f;

        private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

        public override bool CanEnterState => Character.CharacterMovement.LockOnTarget;

        private ITransition CurrentMixer
        {
            get
            {
                Vector2 direction = Character.MovementDirection;

                if (Character.MovementDirection.magnitude > _MagnitudeThreshold)
                {
                    return _DirectionalStrafeJogMovement.GetClip(direction);
                }
                else
                {
                    if (direction == Vector2.zero)
                    {
                        return Character.CurrentAnimationSet.Idle;
                    }

                    return _DirectionalStrafeWalkMovement.GetClip(direction);
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

            Character.CharacterAnimancerComponent.Play(CurrentMixer);
        }

        private void Awake()
        {
            Weapon.OnWeaponChanged += UpdateAnimationSet;
            ChangeAnimationSet();
        }

        private void Update()
        {
            Character.CharacterAnimancerComponent.Play(CurrentMixer);
        }

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        private void ChangeAnimationSet()
        {
            _DirectionalStrafeWalkMovement = Character.CurrentAnimationSet.DirectionalStrafeWalk;
            _DirectionalStrafeJogMovement = Character.CurrentAnimationSet.DirectionalStrafeJog;
        }

        private void OnDestroy()
        {
            Weapon.OnWeaponChanged -= UpdateAnimationSet;
        }

    }
}
