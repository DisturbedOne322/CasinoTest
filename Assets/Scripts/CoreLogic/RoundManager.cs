using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager
{
    public event Action<Player> OnPlayerTurnChanged;
    public event Action OnTurnsEnded;

    private BetsManager _betsManager;
    private List<Player> _players;

    private Player _currentPlayer;
    public Player CurrentPlayer => _currentPlayer;

    private bool _activeTurn = false;
    private const float SKIP_TURN_DISPLAY_TIME_TOTAL = 2.5f;

    public RoundManager(Button endTurnButton, BetsManager betsManager)
    {
        _betsManager = betsManager;
        endTurnButton.onClick.AddListener(() => EndTurn());
    }

    public void StartRound(List<Player> players)
    {
        _players = players;
        GameManager.Instance.StartCoroutine(ProcessAllPlayers());
    }

    private IEnumerator ProcessAllPlayers()
    {
        foreach (Player player in _players)
        {
            //player has no money to bet
            if (!player.InGame)
                yield return GameManager.Instance.StartCoroutine(ProcessOutOfGamePlayer(player));
            else
                yield return GameManager.Instance.StartCoroutine(ProcessInGamePlayer(player));
        }
        OnTurnsEnded?.Invoke();
    }

    private IEnumerator ProcessOutOfGamePlayer(Player player)
    {
        float displayTimeLeft = SKIP_TURN_DISPLAY_TIME_TOTAL;
        OnPlayerTurnChanged?.Invoke(player);
        while (displayTimeLeft > 0)
        {
            displayTimeLeft -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ProcessInGamePlayer(Player player)
    {
        _currentPlayer = player;
        _activeTurn = true;

        OnPlayerTurnChanged?.Invoke(_currentPlayer);

        //wait till the player's turn ends
        while (_activeTurn)
        {
            yield return null;
        }
    }

    private void EndTurn() => _activeTurn = false;
}
