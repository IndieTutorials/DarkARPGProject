using RustedGames.Serializable;
using System;
using System.Collections.Generic;

namespace RustedGames
{
    [Serializable]
    public class Vector3ListReference : VariableReference<List<SerializableVector3>>
    {
        public Vector3ListVariable Variable;
        public override IDataVariable<List<SerializableVector3>> m_variable => Variable;

        public Vector3ListReference(List<SerializableVector3> value) : base(value)
        { }
    }
}
