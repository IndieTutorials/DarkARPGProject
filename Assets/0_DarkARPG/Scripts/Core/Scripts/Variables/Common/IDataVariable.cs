using RustedGames.Events;

namespace RustedGames
{
    public interface IDataVariable<T> : IDataVariable, IChangeEvent<T>
    {
        T Value { get; set; }
        T GetValue();
        void SetValue(T value);
        void SetValue(IDataVariable<T> value);
    }

    public interface IDataVariable : IGameEvent
    {
        object ObjectValue { get; set; }
    }
}
