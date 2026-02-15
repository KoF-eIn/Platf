using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _attackRange = 1.5f;

    private float _lastAttackTime;

    private Transform _transform;
    public bool CanAttack => Time.time >= _lastAttackTime + _attackCooldown;

    private void Awake() => _transform = transform;

    public bool IsInRange(Vector3 targetPosition) =>
        Vector2.Distance(_transform.position, targetPosition) <= _attackRange;

    public void Attack(GameObject target)
    {
        if (!CanAttack) return;

        if (target == null) return;

        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(_damage);
            _lastAttackTime = Time.time;
        }
    }
}