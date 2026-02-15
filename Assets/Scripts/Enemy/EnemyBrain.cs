using UnityEngine;

[RequireComponent(typeof(TargetFinder))]
[RequireComponent(typeof(Follower))]
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Enemy))]
public class EnemyBrain : MonoBehaviour
{
    private TargetFinder _finder;
    private Follower _follower;
    private Patroller _patroller;
    private EnemyAttacker _attacker;
    private Health _health;

    private void Awake()
    {
        _finder = GetComponent<TargetFinder>();
        _follower = GetComponent<Follower>();
        _patroller = GetComponent<Patroller>();
        _attacker = GetComponent<EnemyAttacker>();
        _health = GetComponent<Health>();
    }

    private void OnEnable() => _health.Died += OnDied;
    private void OnDisable() => _health.Died -= OnDied;

    private void Update()
    {
        if (_finder.HasTarget)
        {
            if (_attacker.CanAttack && _attacker.IsInRange(_finder.Target.position))
            {
                _attacker.Attack(_finder.Target.gameObject);
            }
            else
            {
                _follower.Follow(_finder.Target);
            }
        }
        else
        {
            _patroller.Patrol();
        }
    }

    private void OnDied() => Destroy(gameObject);
}