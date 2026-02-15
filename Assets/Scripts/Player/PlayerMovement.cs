using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flip))]
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

    private Flip _flip;

    private PlayerAnimator _playerAnimator;

    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flip = GetComponent<Flip>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable() => _inputService.JumpPressed += OnJumpPressed;
    private void OnDisable() => _inputService.JumpPressed -= OnJumpPressed;

    private void OnJumpPressed()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);

        if (_isGrounded)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void FixedUpdate()
    {
        float horizontal = _inputService.Horizontal;

        _rigidbody.velocity = new Vector2(horizontal * _moveSpeed, _rigidbody.velocity.y);
        _flip.UpdateDirection(horizontal);
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