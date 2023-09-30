using System;

namespace RustedGames
{
    [Serializable]
    public class KeyedVariable
    {
        public string Key;
        public DataVariable Variable;
        public DataVariable Default;
    }
}
