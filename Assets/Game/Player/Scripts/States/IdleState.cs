using UnityEngine;
using Library.FSM;

namespace Game.Player
{
    public class IdleState : State<Player>
    {
        public IdleState(Player context) : base(context)
        {
            
        }

        public override void Enter()
        {
            // Susbscribe to input events
            Context.InputListener.Moved.AddListener(OnMoved);
            Context.InputListener.Jumped.AddListener(OnJumped);
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {
            // Remove listeners
            Context.InputListener.Moved.RemoveListener(OnMoved);
            Context.InputListener.Jumped.RemoveListener(OnJumped);
        }

        private void OnMoved(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                State<Player> newState = new RunState(Context, direction);
                Context.StateMachine.SetState(newState);
            }
        }

        private void OnJumped()
        {
            State<Player> newState = new JumpState(Context);
            Context.StateMachine.SetState(newState);
        }
    }
}
