using Library.FSM;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(InputListener))]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public InputListener InputListener { get; private set; }

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            StateMachine.Debug = true;
            InputListener = GetComponent<InputListener>();
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
