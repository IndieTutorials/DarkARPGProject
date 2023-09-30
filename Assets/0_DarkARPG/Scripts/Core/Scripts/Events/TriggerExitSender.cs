using RustedGames.Serializable;
using UnityEngine;

namespace RustedGames.Events
{
    [AddComponentMenu("System Core/Events/Trigger Exit Sender")]
    public class TriggerExitSender : MonoBehaviour
    {
        [Header("Game Event")]
        public ColliderGameEvent PhysicsEvent;

        [Header("Direct Event")]
        public UnityColliderEvent TriggerExited;

        private void OnTriggerExit(Collider other)
        {
            if (PhysicsEvent != null)
                PhysicsEvent.Raise(other);

            TriggerExited.Invoke(other);
        }
    }
}
