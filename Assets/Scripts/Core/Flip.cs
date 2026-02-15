using UnityEngine;

public class Flip : MonoBehaviour
{
    private Transform _transform;

    private bool _facingRight = true;

    private void Awake() => _transform = transform;

    public void UpdateDirection(float horizontalInput)
    {
        if (horizontalInput > 0 && !_facingRight)
            Flipp();
        else if (horizontalInput < 0 && _facingRight)
            Flipp();
    }

    private void Flipp()
    {
        _facingRight = !_facingRight;
        _transform.Rotate(0f, 180f, 0f);
    }
}