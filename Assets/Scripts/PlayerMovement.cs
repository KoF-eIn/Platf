using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerFlip))]
[RequireComponent(typeof(PlayerAnimator))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputService _inputService;

    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _groundCheckRadius = 0.2f;

    [SerializeField] private Transform _groundCheckPoint;

    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;

    private PlayerFlip _playerFlip;

    private PlayerAnimator _playerAnimator;

    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerFlip = GetComponent<PlayerFlip>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);

        if (_inputService.JumpPressed && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private void FixedUpdate()
    {
        float horizontal = _inputService.Horizontal;
        _rigidbody.velocity = new Vector2(horizontal * _moveSpeed, _rigidbody.velocity.y);

        _playerFlip.UpdateDirection(horizontal);
        _playerAnimator.UpdateSpeed(Mathf.Abs(horizontal));
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
        }
    }
}