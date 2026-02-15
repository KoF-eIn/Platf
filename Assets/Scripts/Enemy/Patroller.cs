using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flip))]
public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform _leftLimit;
    [SerializeField] private Transform _rightLimit;
    [SerializeField] private float _patrolSpeed = 2f;

    private Rigidbody2D _rigidbody;
    private Flip _flip;
    private bool _movingRight = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flip = GetComponent<Flip>();
    }

    public void Patrol()
    {
        Vector2 targetPosition = _movingRight ? _rightLimit.position : _leftLimit.position;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * _patrolSpeed, _rigidbody.velocity.y);

        if (Mathf.Abs(direction.x) > 0.01f)
            _flip.UpdateDirection(direction.x);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            _movingRight = !_movingRight;
    }
}