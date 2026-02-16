using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(InputListener), typeof(Animator), typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public InputListener InputListener { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer Sprite { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }

        public bool IsGrounded { get; private set; }

        public float Speed = 8f;
        public float JumpForce = 10f;
        public Transform GroundCheck;
        public float GroundCheckRadius = 0.2f;
        public LayerMask GroundLayerMask;

        [HideInInspector] public Vector2 CurrentDirection;

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            StateMachine.Debug = true;
            InputListener = GetComponent<InputListener>();
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Sprite = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            State<Player> newState = new IdleState(this);
            StateMachine.SetState(newState);
        }

        private void Update()
        {
            IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayerMask);
            StateMachine.Execute();
        }

        public void FlipSprite()
        {
            Sprite.flipX = CurrentDirection.x < 0;
        }
    }
}
