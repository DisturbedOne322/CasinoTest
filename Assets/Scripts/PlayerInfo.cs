using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerName;
    [SerializeField]
    private TextMeshProUGUI _playerBalance;

    public void Initialize(string name, int balance)
    {
        _playerName.text = name;
        _playerBalance.text = balance.ToString();
    }

    public void UpdateBalance(int newBalance)
    {
        _playerBalance.text = newBalance.ToString();
    }
}
