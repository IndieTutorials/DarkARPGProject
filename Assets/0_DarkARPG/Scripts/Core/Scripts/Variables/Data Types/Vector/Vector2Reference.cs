﻿using RustedGames.Serializable;
using System;

namespace RustedGames
{
    [Serializable]
    public class Vector2Reference : VariableReference<SerializableVector2>
    {
        public Vector2Variable Variable;

        public override IDataVariable<SerializableVector2> m_variable => Variable;

        public Vector2Reference(SerializableVector2 value) : base(value)
        { }
    }
}
