using UnityEngine;
using Library.FSM;

namespace Game.Player
{
    public class IdleState : State<Player>
    {
        private bool _hasJumped;

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
            if (_hasJumped)
            {
                State<Player> newState = new JumpState(Context, Context.Direction);
                Context.StateMachine.SetState(newState);
                return;
            }

            if (Context.Direction != Vector2.zero)
            {
                State<Player> newState = new RunState(Context, Context.Direction);
                Context.StateMachine.SetState(newState);
            }
        }

        public override void Exit()
        {
            // Remove listeners
            Context.InputListener.Moved.RemoveListener(OnMoved);
            Context.InputListener.Jumped.RemoveListener(OnJumped);
        }

        private void OnMoved(Vector2 direction)
        {
            Context.Direction = direction;
        }

        private void OnJumped()
        {
            _hasJumped = true;
        }
    }
}
