using UnityEngine;
using System;
using Animancer;

namespace RustedGames
{
    [Serializable]
    public partial class AttackTransition : ClipTransition, ICopyable<AttackTransition>
    {
        public void CopyFrom(AttackTransition copyFrom)
        {
            base.CopyFrom((ClipTransition) copyFrom);            
        }

        public override void Apply(AnimancerState state)
        {
            base.Apply(state);
        }
    }
}