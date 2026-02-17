using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    public class AirControlState : State<Player>
    {        
        public AirControlState(Player context, Vector2 currentDirection) : base(context)
        {
            Context.CurrentDirection = currentDirection;
        }

        public override void Enter()
        {
            Context.InputListener.Moved.AddListener(OnAirMoved);
            Context.FlipSprite();
        }

        public override void Execute()
        {

            if (Context.IsGrounded && Context.CurrentDirection == Vector2.zero)
            {
                State<Player> newState = new IdleState(Context);
                Context.StateMachine.SetState(newState);
            }

            if (Context.IsGrounded && Context.CurrentDirection != Vector2.zero)
            {
                State<Player> newState = new RunState(Context, Context.CurrentDirection);
                Context.StateMachine.SetState(newState);
            }

            float speed = Time.deltaTime * Context.Speed;
            // Need to add an explicit cast as vector3 to makes += work
            Context.transform.position += (Vector3)Context.CurrentDirection * speed;
        }

        public override void Exit()
        {
            Context.InputListener.Moved.RemoveListener(OnAirMoved);
        }

        private void OnAirMoved(Vector2 direction)
        {
            Context.CurrentDirection = direction;
            Context.FlipSprite();
        }
    }
}
