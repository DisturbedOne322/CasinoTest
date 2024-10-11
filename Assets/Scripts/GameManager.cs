using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoundManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Roulette _roulette;

    [SerializeField]
    private RoundManager _roundManager;

    [SerializeField]
    private int _playerAmount = 2;
    private List<Player> _players;

    void Start()
    {
        InitializeGame();
        StartGame();
    }

    private void InitializeGame()
    {
        GameInitializer gameInitializer = new GameInitializer();
        _players = gameInitializer.InitializePlayers(_playerAmount);

        _roundManager.OnTurnsEnded += OnTurnsEnded;
        _roulette.OnSpinEnd += OnSpinEnd;
    }

    private void OnDestroy()
    {
        _roundManager.OnTurnsEnded -= OnTurnsEnded;
        _roulette.OnSpinEnd -= OnSpinEnd;
    }

    private void OnTurnsEnded()
    {
        _roulette.Spin();
    }

    private void OnSpinEnd(int winningNumber)
    {
        ProcessPayouts(winningNumber);
        StartGame();
    }

    private void StartGame()
    {
        _roundManager.StartRound(_players);
    }

    private void ProcessPayouts(int winningNumber)
    {

    }
}
