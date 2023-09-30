using UnityEngine;
using UnityEngine.Events;

namespace RustedGames.Events
{
    [AddComponentMenu("System Core/Events/Vector2 Game Event Listener")]
    public  class Vector2GameEventListener : GameEventListener<Vector2>
    {
        public Vector2GameEvent Event;
        public UnityVector2DataEvent Response;
        public UnityVector2Event UnityEvent;


        public override IGameEvent<Vector2> m_event => Event;

        public override UnityDataEvent<Vector2> m_responce => Response;

        public override UnityEvent<Vector2> m_unityEvent => UnityEvent;
    }
}