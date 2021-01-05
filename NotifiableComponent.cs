using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

public class NotifiableComponent : ComponentBase, IDisposable
{
    private void ReRender() => base.InvokeAsync(StateHasChanged);

    private List<IStateNotifier> _listening;

    protected void Watch<T>(StateNotifier<T> stateNotifier)
    {
        stateNotifier.AddListener(ReRender);
        _listening.Add(stateNotifier);
    }

    public void Dispose()
    {
        foreach (var stateNotifier in _listening)
        {
            stateNotifier.RemoveListener(ReRender);
        }
    }
}