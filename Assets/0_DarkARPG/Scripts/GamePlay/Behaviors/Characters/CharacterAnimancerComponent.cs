using UnityEngine;
using Animancer;
namespace RustedGames
{
    public sealed class CharacterAnimancerComponent : AnimancerComponent
    {
        [SerializeField]
        private Character _Character;
        public Character Character => _Character;

        private void Awake()
        {           
            // Lite version annoyance
            Animancer.OptionalWarning.All.Disable();
            Playable.ApplyFootIK = true;
        }
    }
}
