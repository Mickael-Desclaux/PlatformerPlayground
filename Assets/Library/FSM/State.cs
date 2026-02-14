namespace Library.FSM
{
    public abstract class State<T> : IState where T : class
    {
        protected T Context { get; }

        protected State(T context)
        {
            Context = context;
        }

        public virtual void Enter() {}

        public virtual void Exit() {}

        public virtual void Execute() {}
    }
}