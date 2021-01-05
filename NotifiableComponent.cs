using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazorStateNotifier
{
    public class NotifiableComponent : ComponentBase, IDisposable
    {
        private void ReRender() => base.InvokeAsync(StateHasChanged);

        private readonly List<IStateNotifier> _listening;

        public NotifiableComponent()
        {
            _listening = new List<IStateNotifier>();
        }

        protected void Watch(IStateNotifier stateNotifier)
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

        protected override void OnInitialized()
        {
            var stateNotifiers = GetType()
                .GetProperties()
                .Where(p => p.GetValue(this) is IStateNotifier);

            foreach (var stateNotifierProperty in stateNotifiers)
            {
                var stateNotifier = (IStateNotifier)stateNotifierProperty.GetValue(this);
                Watch(stateNotifier);
            }

            base.OnInitialized();
        }
    }
}

