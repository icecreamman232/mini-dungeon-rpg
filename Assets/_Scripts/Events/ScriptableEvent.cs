using System;
using UnityEngine;

namespace JustGame.Scripts.Events
{
    public class ScriptableEvent<T> : ScriptableObject
    {
        private Action<T> m_listeners;

        public void AddListener(Action<T> toAdd)
        {
            m_listeners += toAdd;
        }

        public void RemoveListener(Action<T> toRemove)
        {
            m_listeners -= toRemove;
        }

        public void Raise(T value)
        {
            m_listeners?.Invoke(value);
        }
    }
}

