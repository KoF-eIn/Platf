using UnityEngine;

public static class PlayerAnimatorData
{
    public static readonly int Speed = Animator.StringToHash("Speed");
}

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void UpdateSpeed(float speed) => _animator.SetFloat(PlayerAnimatorData.Speed, speed);
}