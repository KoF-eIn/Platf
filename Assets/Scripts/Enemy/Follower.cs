using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flip))]
public class Follower : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 3f;

    private Rigidbody2D _rigidbody;
    private Flip _flipController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipController = GetComponent<Flip>();
    }

    public void Follow(Transform target)
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * _chaseSpeed, _rigidbody.velocity.y);

        if (Mathf.Abs(direction.x) > 0.01f)
            _flipController.UpdateDirection(direction.x);
    }
}