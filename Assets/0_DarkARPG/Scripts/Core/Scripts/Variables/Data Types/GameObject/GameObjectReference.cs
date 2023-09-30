using System;
using UnityEngine;

namespace RustedGames
{
    [Serializable]
    public class GameObjectReference : VariableReference<GameObject>
    {
        public GameObjectPointerVariable Variable;

        public override IDataVariable<GameObject> m_variable => Variable;

        public GameObjectReference(GameObject value) : base(value)
        { }
    }
}
