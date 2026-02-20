using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VampirismTargetFinder))]
[RequireComponent(typeof(VampirismTimer))]
[RequireComponent(typeof(Health))]
public class VampirismDamager : MonoBehaviour
{
    [SerializeField] private int _damagePerTick;

    [SerializeField] private float _tickInterval;

    private VampirismTargetFinder _targetFinder;
    private VampirismTimer _timer;
    private Health _health;
    private Coroutine _damageCoroutine;

    private void Awake()
    {
        _targetFinder = GetComponent<VampirismTargetFinder>();
        _timer = GetComponent<VampirismTimer>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _timer.Started += OnAbilityStarted;
        _timer.Ended += OnAbilityEnded;
    }

    private void OnDisable()
    {
        _timer.Started -= OnAbilityStarted;
        _timer.Ended -= OnAbilityEnded;
    }

    private void OnAbilityStarted()
    {
        _damageCoroutine = StartCoroutine(DamageCoroutine());
    }

    private void OnAbilityEnded()
    {
        if (_damageCoroutine != null)
            StopCoroutine(_damageCoroutine);
    }

    private IEnumerator DamageCoroutine()
    {
        while (_timer.IsActive)
        {
            if (_targetFinder.TryGetNearestEnemy(out Transform enemy))
            {
                Health enemyHealth = enemy.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(_damagePerTick);
                    _health.Heal(_damagePerTick);
                }
            }
            yield return new WaitForSeconds(_tickInterval);
        }
    }
}