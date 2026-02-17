using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private InputService _inputService;

    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 0.5f;

    [SerializeField] private int _damage = 20;

    private float _lastAttackTime;

    private Transform _transform;

    private void Awake() => _transform = transform;

    private void OnEnable() => _inputService.AttackPressed += OnAttackPressed;
    private void OnDisable() => _inputService.AttackPressed -= OnAttackPressed;

    private void OnAttackPressed()
    {
        if (Time.time < _lastAttackTime + _attackCooldown) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(_transform.position, _attackRange);

        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                Health enemyHealth = hit.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(_damage);
                    _lastAttackTime = Time.time;

                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}