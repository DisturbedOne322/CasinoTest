using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField, Min(2)]
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

    [SerializeField]
    private int _maxBet = 1000;
    public int MaxBet => _maxBet;

    private void OnValidate()
    {
        _maxBet = Mathf.Max(_minBet + 1, _maxBet);
        _maxPlayers = Mathf.Max(_minPlayers + 1, _maxPlayers);
    }
}
