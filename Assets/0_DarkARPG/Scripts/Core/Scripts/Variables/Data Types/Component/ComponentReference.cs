using System;
using UnityEngine;

namespace RustedGames
{
    [Serializable]
    public class ComponentReference : VariableReference<Component>
    {
        public ComponentPointerVariable Variable;
        public override IDataVariable<Component> m_variable => Variable;

        public ComponentReference(Component value) : base(value)
        { }
    }
}
