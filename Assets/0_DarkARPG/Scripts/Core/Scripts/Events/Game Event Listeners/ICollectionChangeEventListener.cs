﻿using System.Collections.Generic;

namespace RustedGames.Events
{
    public interface ICollectionChangeEventListener<T> : IChangeEventListener<List<T>>
    {
        void OnEventRaised(CollectionChangeEventData<T> data);
    }
}
