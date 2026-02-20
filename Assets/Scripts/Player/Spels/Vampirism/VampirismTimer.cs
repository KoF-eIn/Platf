using System;
using UnityEngine;

public class VampirismTimer : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;

    public event Action Started;
    public event Action<float> Tick;
    public event Action Ended;
    public event Action CooldownStarted;
    public event Action CooldownEnded;

    private float _currentTime;

    private bool _isActive;
    private bool _isOnCooldown;

    public bool IsActive => _isActive;
    public bool IsOnCooldown => _isOnCooldown;
    public float NormalizedProgress => _isActive ? _currentTime / _duration : 0f;
    public float NormalizedCooldown => _isOnCooldown ? 1f - (_currentTime / _cooldown) : 1f;

    private void Update()
    {
        if (!_isActive && !_isOnCooldown) return;

        _currentTime -= Time.deltaTime;
        Tick?.Invoke(_currentTime);

        if (_isActive && _currentTime <= 0f)
        {
            _isActive = false;
            _isOnCooldown = true;
            _currentTime = _cooldown;
            CooldownStarted?.Invoke();
            Ended?.Invoke();
        }
        else if (_isOnCooldown && _currentTime <= 0f)
        {
            _isOnCooldown = false;
            CooldownEnded?.Invoke();
        }
    }

    public void StartAbility()
    {
        if (_isActive || _isOnCooldown) return;

        _isActive = true;
        _currentTime = _duration;
        Started?.Invoke();
    }
}