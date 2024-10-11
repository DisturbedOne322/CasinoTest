using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Button _endTurnButton;

    public event Action OnTurnsEnded;

    private List<Player> _players;
    private Player _currentPlayer;

    private BetsManager _betsManager;

    private void Awake()
    {
        _betsManager = new();
        _endTurnButton.onClick.AddListener(() => EndTurn());
    }

    public void StartRound(List<Player> players)
    {
        _players = players;
        _betsManager.StartNewRound();
        StartCoroutine(ProcessAllPlayers());
    }

    private IEnumerator ProcessAllPlayers()
    {
        foreach (Player player in _players)
        {
            _currentPlayer = player;
            //wait till the player's turn ends
            while(_currentPlayer != null)
            {
                yield return null;
            }
        }

        OnTurnsEnded?.Invoke();
    }

    private void EndTurn() => _currentPlayer = null;
}
