using System;
using UnityEngine;

namespace RustedGames
{
    [Serializable]
    public class AnimationCurveReference : VariableReference<AnimationCurve>
    {
        public AnimationCurveVariable Variable;
        public override IDataVariable<AnimationCurve> m_variable => Variable;

        public AnimationCurveReference(AnimationCurve value) : base(value)
        { }
    }
}
