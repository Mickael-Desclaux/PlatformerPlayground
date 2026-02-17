using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    public class JumpState : State<Player>
    {
        private const string _jumpAnimation = "hasJumped";
        
        public JumpState(Player context) : base(context)
        {
            
        }

        public override void Enter()
        {
            Context.HideGroundCheck();
            Context.Animator.SetBool(_jumpAnimation, true);

            Context.Rigidbody2D.linearVelocity = new Vector2(Context.Rigidbody2D.linearVelocityX, Context.JumpForce);
            State<Player> newState = new AirControlState(Context, Context.CurrentDirection);
            Context.StateMachine.SetState(newState);
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {
            
        }
    }
}
