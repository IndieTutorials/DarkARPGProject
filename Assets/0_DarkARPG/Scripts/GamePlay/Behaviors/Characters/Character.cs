using UnityEngine;
using Animancer.FSM;
using GamingIsLove.ORKFramework;
using RustedGames.Data;
using System;

namespace RustedGames
{
    [DefaultExecutionOrder(-1100)]
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterAnimancerComponent _CharacterAnimancerComponent;
        public CharacterAnimancerComponent CharacterAnimancerComponent => _CharacterAnimancerComponent;

        [SerializeField]
        private CharacterMovement _CharacterMovement;
        public CharacterMovement CharacterMovement => _CharacterMovement;

        private Combatant _Combatant;
        public Combatant Combatant => _Combatant;

        [SerializeField]
        private CharacterState _Respawn;
        public CharacterState Respawn => _Respawn;

        [SerializeField]
        private CharacterState _Idle;
        public CharacterState Idle => _Idle;

        [SerializeField]
        private CharacterState _Jump;
        public CharacterState Jump => _Jump;

        public Vector2 MovementDirection => _CharacterMovement.MovementDirection;


        [SerializeField]
        private AnimationSet _DefaultAnimationSet;
        public AnimationSet DefaultAnimationSet => _DefaultAnimationSet;

        [SerializeField]
        private AnimationSet _CurrentAnimationSet;
        public AnimationSet CurrentAnimationSet => _CurrentAnimationSet;            


        [SerializeField]
        public StateMachine<CharacterState>.WithDefault StateMachine;
        //= new StateMachine<CharacterState>.WithDefault();

        protected virtual void Awake()
        {

            _CurrentAnimationSet = _DefaultAnimationSet;

            StateMachine.InitializeAfterDeserialize();
            StateMachine.DefaultState = _Respawn; // Start in the Respawn state if there is one.
            StateMachine.DefaultState = _Idle; // But the actual default state is the Idle
        }

        private void Start()
        {
            _Combatant = ORKComponentHelper.GetCombatant(gameObject);
        }

        private void Update()
        {
            if (GamingIsLove.Makinom.InputKey.GetButton(GamingIsLove.Makinom.Maki.InputKeys.GetAsset(
                ORKInputKeys.SPRINT_JUMP)))
            {
                StateMachine.TrySetState(_Jump);
            }
        }

        public bool HasStamina
        {
            get
            {
                return _Combatant.Status[ORKStatusValue.STAMINA].GetValue() > 0;
            }
        }

        public void SetCurrentAnimationSet(AnimationSet theAnimationSet)
        {
           _CurrentAnimationSet = theAnimationSet;
        }

        public void SetDefaultAnimationSet()
        {
            _CurrentAnimationSet = _DefaultAnimationSet;
        }

    }
}
