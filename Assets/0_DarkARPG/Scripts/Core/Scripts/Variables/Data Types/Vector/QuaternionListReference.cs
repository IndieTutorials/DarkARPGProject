﻿using RustedGames.Serializable;
using System;
using System.Collections.Generic;

namespace RustedGames
{
    [Serializable]
    public class QuaternionListReference : VariableReference<List<SerializableQuaternion>>
    {
        public QuaternionListVariable Variable;
        public override IDataVariable<List<SerializableQuaternion>> m_variable => Variable;

        public QuaternionListReference(List<SerializableQuaternion> value) : base(value)
        { }
    }
}
