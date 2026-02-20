using UnityEngine;

[RequireComponent(typeof(VampirismTimer))]
public class VampirismInput : MonoBehaviour
{
    [SerializeField] private KeyCode _activateKey = KeyCode.V;

    private VampirismTimer _timer;

    private void Awake()
    {
        _timer = GetComponent<VampirismTimer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_activateKey))
            _timer.StartAbility();
    }
}