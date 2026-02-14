using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine;

        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            StateMachine.Debug = true;
        }

        private void Start()
        {
            StateMachine.SetState<IdleState>(this);
        }

        private void Update()
        {
            StateMachine.Execute();
        }
    }
}
