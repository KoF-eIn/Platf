using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 0.5f;

    private float _lastAttackTime;
    private bool _isPlayer;
    private bool _isEnemy;

    private void Awake()
    {
        _isPlayer = GetComponent<Player>() != null;
        _isEnemy = GetComponent<Enemy>() != null;
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryAttack(collision.gameObject);
    private void OnCollisionStay2D(Collision2D collision) => TryAttack(collision.gameObject);
    private void OnTriggerEnter2D(Collider2D other) => TryAttack(other.gameObject);
    private void OnTriggerStay2D(Collider2D other) => TryAttack(other.gameObject);

    private void TryAttack(GameObject target)
    {
        if (Time.time < _lastAttackTime + _attackCooldown) return;

        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth == null) return;

        bool canAttack = (_isPlayer && target.GetComponent<Enemy>() != null) ||
                         (_isEnemy && target.GetComponent<Player>() != null);

        if (!canAttack) return;

        targetHealth.TakeDamage(_damage);
        _lastAttackTime = Time.time;
    }
}