using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Text _coinText;

    private int _totalCoins;

    public void AddCoin(int value)
    {
        _totalCoins += value;

        if (_coinText != null)
            _coinText.text = "Coins: " + _totalCoins;
    }
}