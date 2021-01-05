using System;

namespace BlazorStateNotifier
{
    public interface IStateNotifier
    {
        void AddListener(Action listener);
        void RemoveListener(Action listener);
    }

    public abstract class StateNotifier<T> : IStateNotifier
    {
        public StateNotifier(T initialState)
        {
            State = initialState;
        }

        private event Action Listeners;

        private T _state;
        public T State
        {
            get => _state;
            protected set => SetState(value);
        }

        protected void SetState(T newState)
        {
            _state = newState;
            Listeners?.Invoke();
        }

        public void AddListener(Action listener)
        {
            Listeners += listener;
        }

        public void RemoveListener(Action listener)
        {
            Listeners -= listener;
        }
    }
}
