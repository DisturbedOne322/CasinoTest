using System.Collections.Generic;
using UnityEngine;

public class LobbyDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform _displayPlayersParent;
    [SerializeField]
    private GameObject _playerDataDisplayPrefab;

    private List<PlayerLobbyDisplay> _playerDisplayObjectList = new();

    // Start is called before the first frame update
    void Start()
    {
        Lobby.Instance.OnPlayerConnected += Instance_OnPlayerConnected;
        Lobby.Instance.OnPlayerDisconnected += Instance_OnPlayerDisconnected;
    }

    private void OnDestroy()
    {
        Lobby.Instance.OnPlayerConnected -= Instance_OnPlayerConnected;
        Lobby.Instance.OnPlayerDisconnected -= Instance_OnPlayerDisconnected;
    }

    private void Instance_OnPlayerConnected(Player player)
    {

        var playerDisplayGO = Instantiate(_playerDataDisplayPrefab);

        var playerDisplayData = playerDisplayGO.GetComponent<PlayerLobbyDisplay>();
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
