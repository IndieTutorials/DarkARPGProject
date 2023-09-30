using RustedGames.Serializable;
using System;

namespace RustedGames
{
    [Serializable]
    public class RectTransformReference : VariableReference<SerializableRectTransform>
    {
        public RectTransformVariable Variable;

        public override IDataVariable<SerializableRectTransform> m_variable => Variable;

        public RectTransformReference(SerializableRectTransform value) : base(value)
        { }
    }
}
