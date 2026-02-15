using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(InputListener), typeof(Animator))]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public InputListener InputListener { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer Sprite;

        public float Speed = 8;

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            StateMachine.Debug = true;
            InputListener = GetComponent<InputListener>();
            Animator = GetComponent<Animator>();
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
