using System;
using UnityEngine;

namespace RustedGames
{
    [Serializable]
    public class TextureReference : VariableReference<Texture>
    {
        public TexturePointerVariable Variable;
        public override IDataVariable<Texture> m_variable => Variable;

        public TextureReference(Texture value) : base(value)
        { }
    }
}
