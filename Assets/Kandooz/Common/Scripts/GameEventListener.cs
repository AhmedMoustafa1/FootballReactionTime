using Malee;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kandooz
{

    [System.Serializable]
    public class EventList : ReorderableArray<GameEvent>
    {
    }
    public class GameEventListener : MonoBehaviour
    {
        
        [HideInInspector]
        public bool isMultiple =false;
        [HideInInspector]
        public GameEvent Event;
        [HideInInspector]
        [Reorderable]
        public EventList Events;
        [HideInInspector]
        public UnityEvent Response;

        private void OnEnable()
        {
            if (isMultiple)
            {
                for (int i = 0; i < Events.Count; i++)
                {
                    Events[i].RegisterListener(this);
                }
            }
            else
            {

                Event.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            if (isMultiple)
            {
                for (int i = 0; i < Events.Count; i++)
                {

                    Events[i].UnregisterListener(this);
                }
            }
            else
            {

                Event.UnregisterListener(this);
            }
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}