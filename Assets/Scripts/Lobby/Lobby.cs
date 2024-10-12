using System;
using System.Collections.Generic;
using UnityEngine;

// lobby contains players and persists between scene changes
public class Lobby : MonoBehaviour
{
    public event Action<Player> OnPlayerConnected;
    public event Action<Player> OnPlayerDisconnected;

    [SerializeField]
    private GameSettingsSO _gameSettings;

    private static Lobby _instance;
    public static Lobby Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private List<Player> _playersInLobby = new();
    public List<Player> PlayersInLobby => _playersInLobby;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (_instance != this)
            Destroy(gameObject);
    }

    public bool IsLobbyFull() => _playersInLobby.Count >= _gameSettings.MaxPlayers;
    public bool IsLobbyReady() => _playersInLobby.Count >= _gameSettings.MinPlayers;

    public void ConnectPlayer(Player newPlayer)
    {
        if (IsLobbyFull())
            return;

        _playersInLobby.Add(newPlayer);
        OnPlayerConnected?.Invoke(newPlayer);
    }

    public void DisconnectPlayer(Player player)
    {
        if(_playersInLobby.Contains(player))
        {
            _playersInLobby.Remove(player);
            OnPlayerDisconnected?.Invoke(player);
        }
    }
}
