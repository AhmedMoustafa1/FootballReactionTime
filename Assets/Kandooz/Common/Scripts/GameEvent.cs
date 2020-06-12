
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kandooz
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        public UnityEvent onRaise;
        private readonly List<GameEventListener> eventListeners =
            new List<GameEventListener>();
        public void Raise()
        {

            for (int i = eventListeners.Count - 1; i >= 0; i--)
                try
                {
                    eventListeners[i].OnEventRaised();
                }
                catch
                {

                }
            try
            {
                onRaise.Invoke();
            }
            catch
            {

            }

        }
        public void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }
        public void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}