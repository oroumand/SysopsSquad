using System.Collections.Concurrent;

namespace SysopsSquad.Monolithic.Infrastructure
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, List<Action<object>>> _subscribers = new();

        public void Publish<T>(T @event)
        {
            if (@event == null) return;
            var eventType = typeof(T);

            if (_subscribers.TryGetValue(eventType, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler(@event);
                }
            }
        }

        public void Subscribe<T>(Action<T> handler)
        {
            var eventType = typeof(T);
            var action = new Action<object>(e => handler((T)e));

            _subscribers.AddOrUpdate(eventType,
                _ => new List<Action<object>> { action },
                (_, list) => { list.Add(action); return list; });
        }
    }
}
