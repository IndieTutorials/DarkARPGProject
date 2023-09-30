using UnityEngine;
using UnityEngine.InputSystem;

namespace RustedGames
{
    public sealed class PlayerInputBrain : CharacterBrain
    {
        [SerializeField]
        private GameInputMap _Input;
        public GameInputMap Input => _Input;

        private static Vector2 _Movement;
        private Vector2 _Look;

        [Header("Actions")]
        [SerializeField]
        private CharacterState _Jump;
        public CharacterState Jump => _Jump;
        private CharacterState _CurrentJumpState;

        [Header("Makinom Inputs")]
        [SerializeField]
        private GamingIsLove.Makinom.InputKeyAsset _HorizontalMoveKey;

        [SerializeField]
        private GamingIsLove.Makinom.InputKeyAsset _VerticalMoveKey;


        private void Awake()
        {
            if (_Input == null)
            {
                _Input = new GameInputMap();
            }
        }

        private void OnEnable()
        {
            _Input.Move.Move.performed += OnMovement;
            _Input.Move.Move.canceled += OnMovement;

            _Input.Camera.Look.performed += OnLook;
            _Input.Camera.Look.canceled += OnLook;

            _Input.Enable();
        }

        private void OnDisable()
        {
            _Input.Move.Move.performed -= OnMovement;
            _Input.Move.Move.canceled -= OnMovement;

            _Input.Camera.Look.performed -= OnLook;
            _Input.Camera.Look.canceled -= OnLook;

            _Input.Disable();
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            _Movement = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            _Look = context.ReadValue<Vector2>();
        }


        private void Update()
        {
            if (_Jump != null)
            {
                if (GamingIsLove.Makinom.InputKey.GetButton(GamingIsLove.Makinom.Maki.InputKeys.GetAsset(
        ORKInputKeys.SPRINT_JUMP)) && Character.StateMachine.TrySetState(_Jump))
                {
                    _CurrentJumpState = Character.StateMachine.CurrentState;
                }
            }

            /*   Character.CharacterMovement.MovementDirection = new Vector2(
                               GamingIsLove.Makinom.InputKey.GetAxis(_HorizontalMoveKey),
                               GamingIsLove.Makinom.InputKey.GetAxis(_VerticalMoveKey));*/
            Character.CharacterMovement.MovementDirection = new Vector2(
                              _Movement.x,
                              _Movement.y);

            if (GamingIsLove.Makinom.InputKey.GetButton(
                    GamingIsLove.Makinom.Maki.InputKeys.GetAsset(ORKInputKeys.LOCKONTARGET)))
            {
                Character.CharacterMovement.LockOnTarget = !Character.CharacterMovement.LockOnTarget;
            }

            if (!GamingIsLove.Makinom.InputKey.GetButton(
                        GamingIsLove.Makinom.Maki.InputKeys.GetAsset(ORKInputKeys.SPRINT)))
            {
                GamingIsLove.Makinom.Maki.Game.GetObjectVariables(GameConstants.PlayerObjectVariableId).
                    Set(GameConstants.SprintObjectVariableKey, false);
            }

        }

        #region MakinORK static functions

        public static float GetHorizontalAxis()
        {
            return _Movement.x;
        }

        public static float GetVerticalAxis()
        {
            return _Movement.y;
        }

        #endregion

    }
}
