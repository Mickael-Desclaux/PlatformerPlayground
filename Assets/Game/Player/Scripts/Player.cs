using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(InputListener), typeof(Animator), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public InputListener InputListener { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer Sprite;
        public Rigidbody2D Rigidbody2D;

        public float Speed = 8f;
        public float JumpForce = 10f;
        public Vector2 Direction;
        public bool IsGrounded;

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            StateMachine.Debug = true;
            InputListener = GetComponent<InputListener>();
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            State<Player> newState = new IdleState(this);
            StateMachine.SetState(newState);
        }

        private void Update()
        {
            StateMachine.Execute();
        }
    }
}
