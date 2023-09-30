using UnityEngine;
using Cinemachine;
namespace RustedGames
{
    public sealed class CinemachineController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineBrain _CinemachineBrain;

        [SerializeField]
        private CinemachineFreeLook _FreeLookCamera;

        [SerializeField]
        private CinemachineFreeLook _LockTargetCamera;

        public void LockCamera()
        {
            _FreeLookCamera.Priority = 0;
            _LockTargetCamera.Priority = 1;
        }

        public void FreeCamera()
        {
            _FreeLookCamera.Priority = 1;
            _LockTargetCamera.Priority = 0;
        }

        public void SetLookAt(Transform transform)
        {
            _LockTargetCamera.LookAt = transform;
        }

        public void FollowTarget(Transform transform)
        {
            _LockTargetCamera.Follow = transform;
        }

    }
}
