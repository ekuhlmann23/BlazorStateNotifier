using System;

public interface IStateNotifier
{
    void AddListener(Action listener);
    void RemoveListener(Action listener);
}

public abstract class StateNotifier<T> : IStateNotifier
{ 
    StateNotifier(T initialState)
    {
        State = initialState;
    }

    private event Action Listeners;

    private T _state;
    protected T State {
        get => _state;
        set => SetState(value);
    }

    protected void SetState(T newState)
    {
        _state = newState;
        Listeners.Invoke();
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