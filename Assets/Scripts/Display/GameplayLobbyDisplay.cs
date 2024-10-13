using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLobbyDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform _displayPlayersParent;
    [SerializeField]
    private GameObject _playerDataDisplayPrefab;

    private List<PlayerGameDisplayInfo> _playerDisplayObjectList = new();

    // Start is called before the first frame update
    void Start()
    {
        Lobby.Instance.OnPlayerDisconnected += Instance_OnPlayerDisconnected;
        InstantiatePlayers();
    }

    private void OnDestroy()
    {
        Lobby.Instance.OnPlayerDisconnected -= Instance_OnPlayerDisconnected;
    }

    //after the game loads, instantiate all the players in the lobby.
    private void InstantiatePlayers()
    {
        var playersInLobby = Lobby.Instance.PlayersInLobby;
        if (playersInLobby.Count == 0)
            return;

        foreach (var player in playersInLobby)
        {
            InstantiateAndAddPlayerDisplay(player);
        }
    }

    private void InstantiateAndAddPlayerDisplay(Player player)
    {
        var playerDisplayGO = Instantiate(_playerDataDisplayPrefab);

        var playerDisplayData = playerDisplayGO.GetComponent<PlayerGameDisplayInfo>();
        playerDisplayData.Initialize(player);
        playerDisplayData.transform.SetParent(transform, false);

        _playerDisplayObjectList.Add(playerDisplayData);
    }

    private void Instance_OnPlayerDisconnected(Player player)
    {
        for (int i = 0; i < _playerDisplayObjectList.Count; i++)
        {
            if (_playerDisplayObjectList[i].Player != player)
                continue;

            Destroy(_playerDisplayObjectList[i].gameObject);
            _playerDisplayObjectList.RemoveAt(i);
        }
    }
}
