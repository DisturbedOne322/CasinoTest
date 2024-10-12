using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField, Min(1)]
    private int _minPlayers = 2;
    public int MinPlayers => _minPlayers;

    [SerializeField]
    private int _maxPlayers = 3;
    public int MaxPlayers => _maxPlayers;

    [SerializeField]
    private int _defaultBalance = 1000;
    public int DefaultBalance => _defaultBalance;

    [SerializeField, Min(1)]
    private int _minBet = 25;
    public int MinBet => _minBet;

    [SerializeField, Min(2)]
    private int _maxBet = 1000;
    public int MaxBet => _maxBet;

    [SerializeField, Min(1)]
    private float _payoutMultiplier = 1;
    public float PayoutMultiplier => _payoutMultiplier;

    private void OnValidate()
    {
        _maxPlayers = Mathf.Max(_minPlayers + 1, _maxPlayers);
    }
}
