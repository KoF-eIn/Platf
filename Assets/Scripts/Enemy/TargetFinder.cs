using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private float _visionRange = 5f;

    [SerializeField] private LayerMask _obstacleLayers;

    private Transform _transform;
    private Transform _target;
    public bool HasTarget => _target != null;
    public Transform Target => _target;

    private void Awake() => _transform = transform;

    private void Update()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_transform.position, _visionRange);
        Transform foundPlayer = null;

        foreach (var hit in hits)
        {
            if (hit.GetComponent<Player>() != null)
            {
                foundPlayer = hit.transform;

                break;
            }
        }

        if (foundPlayer != null && HasLineOfSight(foundPlayer))
        {
            _target = foundPlayer;
        }
        else
        {
            _target = null;
        }
    }

    private bool HasLineOfSight(Transform player)
    {
        Vector2 direction = player.position - _transform.position;
        float distance = direction.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, direction, distance, _obstacleLayers);

        return hit.collider == null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _visionRange);
    }
}