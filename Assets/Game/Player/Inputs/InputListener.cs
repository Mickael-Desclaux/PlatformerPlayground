using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class InputListener : MonoBehaviour
    {
        public UnityEvent<Vector2> Moved;
        public UnityEvent Jumped;
        public UnityEvent Attacked;

        private void OnMove(InputValue value)
        {
            Vector2 move = value.Get<Vector2>();
            Moved?.Invoke(move);
        }

        private void OnJump()
        {
            Jumped?.Invoke();
        }

        private void OnAttack()
        {
            Attacked?.Invoke();
        }
    }
}
