namespace Library.FSM
{
    internal interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}