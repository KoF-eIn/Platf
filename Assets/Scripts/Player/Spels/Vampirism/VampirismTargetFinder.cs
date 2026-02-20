using UnityEngine;

public class VampirismTargetFinder : MonoBehaviour
{
    [SerializeField] private float _radius;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public bool TryGetNearestEnemy(out Transform enemy)
    {
        enemy = null;
        Collider2D[] hits = Physics2D.OverlapCircleAll(_transform.position, _radius);
        float minDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() == null) continue;

            float distance = Vector2.Distance(_transform.position, hit.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                enemy = hit.transform;
            }
        }

        return enemy != null;
    }
}