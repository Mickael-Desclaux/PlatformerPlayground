using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    public class RunState : State<Player>
    {
        private const string _runningAnimation = "isRunning";
        private Vector2 _direction;

        public RunState(Player context, Vector2 currentDirection) : base(context)
        {
            _direction = currentDirection;
        }

        public override void Enter()
        {
            Context.InputListener.Moved.AddListener(OnMoved);
            Context.Animator.SetBool(_runningAnimation, true);
            FlipSprite();
        }

        public override void Execute()
        {
            if (_direction == Vector2.zero)
            {
                State<Player> newState = new IdleState(Context);
                Context.StateMachine.SetState(newState);
            }

            float speed = Time.deltaTime * Context.Speed;
            // Need to add an explicit cast as vector3 to makes += work
            Context.transform.position += (Vector3)_direction * speed;

        }

        public override void Exit()
        {
            Context.InputListener.Moved.RemoveListener(OnMoved);
            Context.Animator.SetBool(_runningAnimation, false);
        }

        private void OnMoved(Vector2 direction)
        {
            _direction = direction;
        }

        private void FlipSprite()
        {
            Context.Sprite.flipX = _direction.x < 0;
        }
    }
}
