using System;
using UnityEngine;

namespace RustedGames
{
    public sealed class CharacterMovement : MonoBehaviour
    {

        [SerializeField] private Vector2 _MoveDirection;
        public Vector2 MovementDirection
        {
            get => _MoveDirection;
            set
            {
                _MoveDirection.x = value.x;
                _MoveDirection.y = value.y;
            }
        }

        public bool CanRoll = false;
        public bool CanBackstep = false;
        public bool Dodge = false;
        public bool Sprint = false;

        [SerializeField] private Transform _CameraTransform;
        [SerializeField] private float _RotationSeed = 300f;
        private float _YSpeed;

        [Header("Temp Target")]
        [SerializeField]
        private Transform TargetLock;
        public bool LockOnTarget = false;
        [SerializeField]
        private Cinemachine.CinemachineTargetGroup _CinemachineTargetGroup;
        [SerializeField]
        private Transform _PlayerCameraLook;


        [SerializeField]
        private CharacterAnimancerComponent _Animancer;
        public CharacterAnimancerComponent Animancer => _Animancer;

        [SerializeField]
        private CharacterController _CharacterController;
        public CharacterController CharacterController => _CharacterController;

        [SerializeField]
        private GamingIsLove.Footsteps.Footstepper _Footstepper;
        public GamingIsLove.Footsteps.Footstepper FootStepper => _Footstepper;

        private GamingIsLove.Makinom.VariableHandler _VariableHandler;
        public GamingIsLove.Makinom.VariableHandler VariableHandler => _VariableHandler;


        private void Awake()
        {
            _CameraTransform = Camera.main.transform;
            _VariableHandler = GamingIsLove.Makinom.Maki.Game.GetObjectVariables(GameConstants.PlayerObjectVariableId);
        }
        private void Update()
        {

            _Footstepper.SetGrounded(_CharacterController.isGrounded);

            HandleMovement();

            if (LockOnTarget)
            {
                HandleTargetLockedRotation();
            }
            else
            {
                HandleRotation();
            }



            Vector3 moveDirection = Vector3.forward * _MoveDirection.y + Vector3.right * _MoveDirection.x;

            moveDirection = Quaternion.AngleAxis(_CameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            moveDirection.Normalize();

            // transform.position += moveDirection * 5f * Time.deltaTime;
            _YSpeed += Physics.gravity.y * Time.deltaTime;
        }

        private void HandleTargetLockedRotation()
        {
            Vector3 rotationOffset = TargetLock.transform.position - transform.position;
            rotationOffset.y = 0f;
            transform.forward += Vector3.Lerp(transform.forward, rotationOffset, Time.deltaTime * _RotationSeed);
        }

        private void HandleRotation()
        {
            Vector3 rotationOffset = _CameraTransform.TransformDirection(new Vector3(_MoveDirection.x, 0, _MoveDirection.y));
            rotationOffset.y = 0f;
            transform.forward += Vector3.Lerp(transform.forward, rotationOffset, Time.deltaTime * _RotationSeed);
        }

        private void HandleMovement()
        {
            Dodge = _VariableHandler.GetBool(GameConstants.DodgeObjectVariableKey);
            Sprint = _VariableHandler.GetBool(GameConstants.SprintObjectVariableKey);

            UpdateLockTargets();

        }

        private void UpdateLockTargets()
        {
            if (LockOnTarget)
            {
                TargetLock = FindObjectOfType<TargetComponent>().gameObject.transform;
                _CinemachineTargetGroup.AddMember(transform, 0.9f, 0f);
                _CinemachineTargetGroup.AddMember(TargetLock, 0.6f, 0f);

                _CameraTransform.gameObject.GetComponent<CinemachineController>().LockCamera();
                _CameraTransform.gameObject.GetComponent<CinemachineController>().FollowTarget(_PlayerCameraLook);
                _CameraTransform.gameObject.GetComponent<CinemachineController>().SetLookAt(_CinemachineTargetGroup.transform);

            }
            else
            {
                _CameraTransform.gameObject.GetComponent<CinemachineController>().FreeCamera();
                _CinemachineTargetGroup.RemoveMember(transform);
                _CinemachineTargetGroup.RemoveMember(TargetLock);
            }
        }


        public void OnRollOrBackstep(bool bActionPressed)
        {
            if (bActionPressed)
            {
                if (_MoveDirection != default)
                {
                    CanRoll = true;
                    CanBackstep = false;
                }
                else
                {
                    CanRoll = false;
                    CanBackstep = true;
                }
            }
        }

        private void OnAnimatorMove()
        {
            Vector3 velocity = _Animancer.Animator.deltaPosition;
            velocity.y = _YSpeed * Time.deltaTime;
            _CharacterController.Move(velocity);
        }

        public void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}