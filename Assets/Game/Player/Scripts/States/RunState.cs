using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    public class RunState : State<Player>
    {
        private const string _runningAnimation = "isRunning";

        public RunState(Player context, Vector2 currentDirection) : base(context)
        {
            Context.CurrentDirection = currentDirection;
        }

        public override void Enter()
        {
            Context.InputListener.Moved.AddListener(OnMoved);
            Context.InputListener.Jumped.AddListener(OnJumped);
            Context.Animator.SetBool(_runningAnimation, true);
            Context.FlipSprite();
        }

        public override void Execute()
        {
            if (Context.CurrentDirection == Vector2.zero)
            {
                State<Player> newState = new IdleState(Context);
                Context.StateMachine.SetState(newState);
            }

            float speed = Time.deltaTime * Context.Speed;
            // Need to add an explicit cast as vector3 to makes += work
            Context.transform.position += (Vector3)Context.CurrentDirection * speed;

        }

        public override void Exit()
        {
            Context.InputListener.Moved.RemoveListener(OnMoved);
            Context.InputListener.Jumped.RemoveListener(OnJumped);
            Context.Animator.SetBool(_runningAnimation, false);
        }

        private void OnMoved(Vector2 direction)
        {
            Context.CurrentDirection = direction;
            Context.FlipSprite();
        }

        private void OnJumped()
        {
            State<Player> newState = new JumpState(Context);
            Context.StateMachine.SetState(newState);
        }
    }
}
