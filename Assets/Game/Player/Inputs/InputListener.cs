using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class InputListener : MonoBehaviour
    {
        public event Action<Vector2> Moved;
        public event Action Jumped;

        private void OnMove(InputValue value)
        {
            Vector2 move = value.Get<Vector2>();
            Debug.LogWarning($"x: {move.x}, y: {move.y}");
            Moved?.Invoke(move);
        }

        private void OnJump()
        {
            Jumped?.Invoke();
            Debug.LogWarning("Jump!");
        }
    }
}
