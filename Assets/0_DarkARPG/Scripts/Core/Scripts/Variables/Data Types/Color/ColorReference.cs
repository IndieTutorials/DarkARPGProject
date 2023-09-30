using RustedGames.Serializable;
using System;

namespace RustedGames
{
    [Serializable]
    public class ColorReference : VariableReference<SerializableColor>
    {
        public ColorVariable Variable;
        public override IDataVariable<SerializableColor> m_variable => Variable;

        public ColorReference(SerializableColor value) : base(value)
        { }
    }
}
