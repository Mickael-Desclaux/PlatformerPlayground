using System.Collections;
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

        public bool IsGrounded 
        { 
            get 
            {
                if (!_groundCheck.gameObject.activeInHierarchy)
                {
                    return false;
                }

                return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayerMask);
            } 
        }

        public float Speed = 8f;
        public float JumpForce = 10f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayerMask;

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
            StateMachine.Execute();
        }

        public void HideGroundCheck()
        {
            _groundCheck.gameObject.SetActive(false);
            StartCoroutine(ShowGroundCheck());
        }

        public void FlipSprite()
        {
            Sprite.flipX = CurrentDirection.x < 0;
        }

        private IEnumerator ShowGroundCheck()
        {
            yield return new WaitForSeconds(0.2f);
            _groundCheck.gameObject.SetActive(true);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
