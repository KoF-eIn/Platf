using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public float speed = 3f;

    private bool movingRight = true;

    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightLimit.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, rightLimit.position) < 0.1f)
                movingRight = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftLimit.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, leftLimit.position) < 0.1f)
                movingRight = true;
        }
    }

    // Опционально: поворот спрайта в зависимости от направления
    private void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}