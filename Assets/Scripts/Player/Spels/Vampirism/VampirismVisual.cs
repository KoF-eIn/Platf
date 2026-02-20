using UnityEngine;
using UnityEngine.UI;

public class VampirismVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _radiusSprite;
    [SerializeField] private Text _timerText;
    [SerializeField] private Slider _timerBar;

    private VampirismTimer _timer;

    private void Awake()
    {
        _timer = GetComponent<VampirismTimer>();
        if (_radiusSprite != null)
            _radiusSprite.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _timer.Started += OnAbilityStarted;
        _timer.Ended += OnAbilityEnded;
        _timer.CooldownStarted += OnCooldownStarted;
        _timer.CooldownEnded += OnCooldownEnded;
        _timer.Tick += OnTimerTick;
    }

    private void OnDisable()
    {
        _timer.Started -= OnAbilityStarted;
        _timer.Ended -= OnAbilityEnded;
        _timer.CooldownStarted -= OnCooldownStarted;
        _timer.CooldownEnded -= OnCooldownEnded;
        _timer.Tick -= OnTimerTick;
    }

    private void OnAbilityStarted()
    {
        if (_radiusSprite != null)
            _radiusSprite.gameObject.SetActive(true);
    }

    private void OnAbilityEnded()
    {
        if (_radiusSprite != null)
            _radiusSprite.gameObject.SetActive(false);
    }

    private void OnCooldownStarted()
    {
        
    }

    private void OnCooldownEnded()
    {
        if (_timerText != null)
            _timerText.text = "V";

        if (_timerBar != null)
            _timerBar.value = 1f;
    }

    private void OnTimerTick(float timeLeft)
    {
        if (_timerText != null)
        {
            if (_timer.IsActive)
                _timerText.text = timeLeft.ToString("F1");
            else if (_timer.IsOnCooldown)
                _timerText.text = timeLeft.ToString("F1");
        }

        if (_timerBar != null)
        {
            if (_timer.IsActive)
                _timerBar.value = _timer.NormalizedProgress;
            else if (_timer.IsOnCooldown)
                _timerBar.value = _timer.NormalizedCooldown;
        }
    }
}