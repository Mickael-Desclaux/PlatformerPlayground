namespace Library.FSM
{
    public class StateMachine<T> where T : class
    {
        private IState _currentState;
        public bool Debug;
        
        public void SetState<TState>(State<T> newState) where TState : State<T>
        {
            TState state = (TState)System.Activator.CreateInstance(typeof(TState), newState);
            InternalSetState(state);
        }

        public void SetStateTest(State<T> newState)
        {
            var stateType = newState.GetType();
            State<T> state = (State<T>)System.Activator.CreateInstance(stateType);
            InternalSetState(state);
        }

        public void Execute()
        {
            _currentState?.Execute();
        }

        private void InternalSetState(IState newState)
        {
            if (Debug)
            {
                string oldState = _currentState?.ToString();
                oldState = string.IsNullOrEmpty(oldState) ? "Empty" : oldState[(oldState.LastIndexOf('.') + 1)..];
                UnityEngine.Debug.Log($"Switching state from {oldState} to {newState.ToString()[(newState.ToString().LastIndexOf('.') + 1)..]}");
            }
            
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

    }
}
