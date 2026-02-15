using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    private Transform _transform;

    private bool _facingRight = true;

    private void Awake()
    {
        _transform = transform;
    }

    public void UpdateDirection(float horizontalInput)
    {
        if (horizontalInput > 0 && !_facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        _transform.Rotate(0f, 180f, 0f);
    }
}