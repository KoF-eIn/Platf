using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(_camera);
        transform.Rotate(0, 180, 0);
    }
}