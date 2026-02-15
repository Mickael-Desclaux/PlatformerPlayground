using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    public class JumpState : State<Player>
    {
        private const string _jumpAnimation = "hasJumped";
        private bool _isGrounded;
        
        public JumpState(Player context, Vector2 currentDirection) : base(context)
        {
            Context.Direction = currentDirection;
        }

        public override void Enter()
        {
            Context.InputListener.Jumped.AddListener(OnJumped);
            Context.Animator.SetBool(_jumpAnimation, true);
            FlipSprite();

            Context.Rigidbody2D.linearVelocity = new Vector2(Context.Rigidbody2D.linearVelocityX, Context.JumpForce);
        }

        public override void Execute()
        {
            if (_isGrounded && Context.Direction == Vector2.zero)
            {
                State<Player> newState = new IdleState(Context);
                Context.StateMachine.SetState(newState);
            }

            if (_isGrounded && Context.Direction != Vector2.zero)
            {
                State<Player> newState = new RunState(Context, Context.Direction);
                Context.StateMachine.SetState(newState);
            }
        }

        public override void Exit()
        {
            Context.InputListener.Jumped.RemoveListener(OnJumped);
        }

        private void OnJumped()
        {
            
        }

        private void FlipSprite()
        {
            Context.Sprite.flipX = Context.Direction.x < 0;
        }
    }
}
