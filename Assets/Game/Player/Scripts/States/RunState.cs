using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    public class RunState : State<Player>
    {
        private Vector2 _direction;

        public RunState(Player context, Vector2 currentDirection) : base(context)
        {
            _direction = currentDirection;
        }

        public override void Enter()
        {
            Context.InputListener.Moved.AddListener(OnMoved);
        }

        public override void Execute()
        {
            if (_direction == Vector2.zero)
            {
                State<Player> newState = new IdleState(Context);
                Context.StateMachine.SetState(newState);
            }
        }

        public override void Exit()
        {
            Context.InputListener.Moved.RemoveListener(OnMoved);
        }

        private void OnMoved(Vector2 direction)
        {
            _direction = direction;
            Debug.LogWarning($"Run, x: {_direction.x}, y: {_direction.y}");
        }
    }
}
