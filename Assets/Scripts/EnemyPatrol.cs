using UnityEngine;

[RequireComponent(typeof(Flip))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _leftLimit;
    [SerializeField] private Transform _rightLimit;

    [SerializeField] private float _speed = 3f;

    private Flip _flipController;

    private bool _movingRight = true;

    private void Awake()
    {
        _flipController = GetComponent<Flip>();
    }

    private void Update()
    {
        if (_movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, _rightLimit.position, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _rightLimit.position) < 0.1f)
                _movingRight = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _leftLimit.position, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _leftLimit.position) < 0.1f)
                _movingRight = true;
        }

        _flipController.UpdateDirection(_movingRight ? 1f : -1f);
    }
}