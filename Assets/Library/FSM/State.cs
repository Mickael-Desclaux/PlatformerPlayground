namespace Library.FSM
{
    public abstract class State<T> : IState where T : class
    {
        protected T Context { get; }

        protected State(T context)
        {
            Context = context;
        }

        public abstract void Enter();

        public abstract void Execute();

        public abstract void Exit();
    }
}