using UnityEngine;
using Animancer;

namespace RustedGames
{
    [DefaultExecutionOrder(DefaultExecutionOrder)]
    public abstract class CharacterBrain : MonoBehaviour
    {
        public const int DefaultExecutionOrder = -10000;

        [SerializeField]
        private Character _Character;
        public ref Character Character => ref _Character;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            this.gameObject.GetComponentInParentOrChildren(ref _Character);
        }
#endif
    }
}
