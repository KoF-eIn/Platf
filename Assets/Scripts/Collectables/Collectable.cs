using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableType _type;
    [SerializeField] private int _value;

    public CollectableType Type => _type;
    public int Value => _value;
}