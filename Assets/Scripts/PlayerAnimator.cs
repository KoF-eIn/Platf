using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _speedHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _speedHash = Animator.StringToHash("Speed");
    }

    public void UpdateSpeed(float speed)
    {
        _animator.SetFloat(_speedHash, speed);
    }
}