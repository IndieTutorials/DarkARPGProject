using System;

namespace RustedGames.Serializable
{
    [Serializable]
    public class KeyedObject
    {
        public KeyedObject() { }
        public string Key;
        public object Data;
    }
    
}
