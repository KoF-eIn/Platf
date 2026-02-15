using UnityEngine;
using System;

public class InputService : MonoBehaviour
{
    public float Horizontal { get; private set; }

    public event Action JumpPressed;
    public event Action AttackPressed;

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            JumpPressed?.Invoke();

        if (Input.GetButtonDown("Fire1"))
            AttackPressed?.Invoke();
    }
}