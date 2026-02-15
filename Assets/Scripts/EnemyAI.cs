using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flip))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Enemy))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _leftLimit;
    [SerializeField] private Transform _rightLimit;

    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 3f;
    [SerializeField] private float _visionRange = 5f;

    [SerializeField] private LayerMask _obstacleLayers;

    private Rigidbody2D _rigidbody;

    private Flip _flipController;

    private Health _health;

    private Transform _player;

    private bool _isChasing;
    private bool _movingRight = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipController = GetComponent<Flip>();
        _health = GetComponent<Health>();

        _rigidbody.gravityScale = 0f;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnEnable() => _health.Died += OnDied;
    private void OnDisable() => _health.Died -= OnDied;

    private void Update()
    {
        FindPlayer();
    }

    private void FixedUpdate()
    {
        float currentSpeed = _isChasing ? _chaseSpeed : _patrolSpeed;
        Vector2 targetPosition;

        if (_isChasing && _player != null)
        {
            targetPosition = _player.position;
        }
        else
        {
            if (_movingRight)
            {
                targetPosition = _rightLimit.position;

                if (Vector2.Distance(transform.position, _rightLimit.position) < 0.1f)
                    _movingRight = false;
            }
            else
            {
                targetPosition = _leftLimit.position;

                if (Vector2.Distance(transform.position, _leftLimit.position) < 0.1f)
                    _movingRight = true;
            }
        }

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * currentSpeed, _rigidbody.velocity.y);

        if (Mathf.Abs(direction.x) > 0.01f)
            _flipController.UpdateDirection(direction.x);
    }

    private void FindPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _visionRange);
        Transform foundPlayer = null;

        foreach (var hit in hits)
        {
            if (hit.GetComponent<Player>() != null)
            {
                foundPlayer = hit.transform;
                break;
            }
        }

        if (foundPlayer != null)
        {
            _player = foundPlayer;
            _isChasing = HasLineOfSightToPlayer();
        }
        else
        {
            _player = null;
            _isChasing = false;
        }
    }

    private bool HasLineOfSightToPlayer()
    {
        if (_player == null) return false;

        Vector2 direction = _player.position - transform.position;
        float distance = direction.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, _obstacleLayers);

        return hit.collider == null;
    }

    private void OnDied() => Destroy(gameObject);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _visionRange);
    }
}